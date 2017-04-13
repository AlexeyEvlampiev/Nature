namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using System;
    using System.Diagnostics;

    public sealed class NasaA7 : NasaA7Base
    {
        private NasaA7Header _header;
        public override NasaA7Header Header => _header;

        public double LowTemperature { get; private set; }
        public double HighTemperature { get; private set; }
        public double CommonTemperature { get; private set; }

        public NasaA7ApproximationRange LowTemperatureRange { get; private set; }

        public NasaA7ApproximationRange HighTemperatureRange { get; private set; }

        public override double ReducedCp(double temperature)
        {
            temperature = temperature < LowTemperature ? LowTemperature : temperature;
            temperature = temperature > HighTemperature ? HighTemperature : temperature;            
            return temperature < CommonTemperature 
                ? LowTemperatureRange.CalcReducedCp(temperature)
                : HighTemperatureRange.CalcReducedCp(temperature);
        }

        public override double ReducedH(double temperature)
        {
            temperature = temperature < 1.0d ? 1.0d : temperature;            
            if (temperature < LowTemperature)
            {
                return ReducedH(LowTemperature) * LowTemperature / temperature
                    - ReducedCp(LowTemperature) * (LowTemperature - temperature) / temperature;
            }
            if (temperature > HighTemperature)
            {
                return ReducedH(HighTemperature) * HighTemperature / temperature
                    + ReducedCp(HighTemperature) * (temperature - HighTemperature) / temperature;
            }

            return temperature < CommonTemperature
                ? LowTemperatureRange.CalcReducedH(temperature)
                : HighTemperatureRange.CalcReducedH(temperature);
        }

        public override double ReducedS(double temperature)
        {
            temperature = temperature < 1.0d ? 1.0d : temperature;
            if (temperature < LowTemperature)
            {
                return ReducedS(LowTemperature) + ReducedCp(LowTemperature) * Math.Log(temperature / LowTemperature);
            }

            if(temperature > HighTemperature)
            {
                return ReducedS(HighTemperature) + ReducedCp(HighTemperature) * Math.Log(temperature / HighTemperature);
            }

            return temperature < CommonTemperature
                ? LowTemperatureRange.CalcReducedS(temperature)
                : HighTemperatureRange.CalcReducedS(temperature);
        }

        public static NasaA7 Parse(
            string text,
            INasaA7DeserializationContext context = null)
        {
            ChemkinMarkup markup = new ChemkinMarkup(text);
            context = context ?? new DefaultDeserializationContext();
            return Parse(markup, context, null);
        }

        internal static NasaA7 Parse(
            ChemkinMarkup markup,
            INasaA7DeserializationContext context,
            Capture capture)
        {
            int index = 0;
            int length = markup.AdaptedText.Length;
            if (ReferenceEquals(capture, null) == false)
            {
                index = capture.Index;
                length = capture.Length;
            }

            var session = context.GetSession();
            var options = context.GetOptions();
            var diagnosticsCallback = context.GetDiagnosticsCallback();
            var formatInfo = context.GetFormatInfo();
            var messageBuilder = context.GetMessageBuilder();

            string urlParams = NasaA7HeaderFormatOptions.BuildUrlParams(options);
            var regex = session.GetOrCreate<Regex>($"nasa-a7-classic/regex?{urlParams}", 
                ()=> 
                {
                    int width = options.SpeciesIdWidth +
                    options.DateWidth +
                    options.ElementsMaxCount * options.ElementWidth + 35;
                    string pattern = $@"
                    ^(?<header>.{{{width}}} [1] .* ) \n
                    ^(?<a>.{{15}}){{5}} (?: (?:.{{4}}  [2].*) | (?<def2>.*)) \n
                    ^(?<a>.{{15}}){{5}} (?: (?:.{{4}}  [3].*) | (?<def3>.*)) \n
                    ^(?<a>.{{15}}){{4}} (?: (?:.{{19}} [4].*) | (?<def4>.*)) ";
                    pattern = RegexUtils.Minify(pattern);
                    return new Regex(pattern, RegexOptions.Multiline);
                });

            var result = new NasaA7();
            var match = regex.Match(markup.AdaptedText, index, length);
            if (!match.Success)
            {
                string message = messageBuilder.InvalidNasaA7Format();
                throw new ChemkinFormatException(index, markup, message);
            }

            var headerCapture = match.Groups["header"];

            result._header = NasaA7Header.Parse(markup, context, headerCapture);

            double? lowTemperature = result._header.LowTemperature ?? context.DefaultLowTemperature;
            double? highTemperature = result._header.HighTemperature ?? context.DefaultHighTemperature;
            double? commonTemperature = result._header.CommonTemperature ?? context.DefaultCommonTemperature;

            if (!lowTemperature.HasValue)
            {
                string message = messageBuilder.MissingLowTemperature();
                throw new ChemkinIntegrityException(headerCapture, markup, message);
            }

            if (!highTemperature.HasValue)
            {
                string message = messageBuilder.MissingHighTemperature();
                throw new ChemkinIntegrityException(headerCapture, markup, message);
            }

            if (!commonTemperature.HasValue)
            {
                string message = messageBuilder.MissingCommonTemperature();
                throw new ChemkinIntegrityException(headerCapture, markup, message);
            }

            result.LowTemperature = lowTemperature.Value;
            result.HighTemperature = highTemperature.Value;
            result.CommonTemperature = commonTemperature.Value;

            var highTempRangeArray = new double[7];
            var lowTempRangeArray = new double[7];
            var coefCaptures = match.Groups["a"].Captures;
            for (int i = 0; i < 14; ++i)
            {
                var coefCapture = coefCaptures[i];
                int range = i / 7;
                int ordinal = i % 7;
                if (formatInfo.IsRealNumber(coefCapture.Value))
                {
                    var value = formatInfo.ToDouble(coefCapture.Value);
                    var temp = range == 0 ? highTempRangeArray : lowTempRangeArray;
                    temp[ordinal] = value;
                }
            }

            var highTempRange= new NasaA7ApproximationRange(highTempRangeArray);
            var lowTempRange = new NasaA7ApproximationRange(lowTempRangeArray);

            var def2 = match.Groups["def2"];
            var def3 = match.Groups["def3"];
            var def4 = match.Groups["def4"];
            if (def2.Success)
            {
                string message = messageBuilder.MissingInputEolField(2);
                diagnosticsCallback.Error(def4, markup, message);
            }

            if (def3.Success)
            {
                string message = messageBuilder.MissingInputEolField(3);
                diagnosticsCallback.Error(def4, markup, message);
            }

            if (def4.Success)
            {
                string message = messageBuilder.MissingInputEolField(4);
                diagnosticsCallback.Error(def4, markup, message);
            }

            if (highTemperature == commonTemperature)
                highTempRange = lowTempRange;


            double tcom = result.CommonTemperature;
            var rebasedHighRange = highTempRange.Rebase(
                    lowTempRange.CalcReducedCp(tcom),
                    lowTempRange.CalcReducedH(tcom),
                    lowTempRange.CalcReducedS(tcom),
                    tcom
                );

            var comparer = DoubleEqualityComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 3.0e-2);
            double lowValue = lowTempRange.CalcReducedCp(tcom);
            double highValue = highTempRange.CalcReducedCp(tcom);

            if (false == comparer.Equals(lowValue, highValue))
            {
                string message = messageBuilder.CpDiscontinuity(result.Header.SpeciesCode, rebasedHighRange.A1, highTempRange.A1);
                throw new ChemkinIntegrityException(coefCaptures[7], markup, message);
            }

            lowValue = lowTempRange.CalcReducedH(tcom);
            highValue = highTempRange.CalcReducedH(tcom);

            if (false == comparer.Equals(lowValue, highValue))
            {
                string message = messageBuilder.EnthalpyDiscontinuity(result.Header.SpeciesCode, rebasedHighRange.A6, highTempRange.A6);
                throw new ChemkinIntegrityException(coefCaptures[12], markup, message);
            }


            lowValue = lowTempRange.CalcReducedS(tcom);
            highValue = highTempRange.CalcReducedS(tcom);

            if (false == comparer.Equals(lowValue, highValue))
            {
                string message = messageBuilder.EntropyDiscontinuity(result.Header.SpeciesCode, rebasedHighRange.A7, highTempRange.A7);
                throw new ChemkinIntegrityException(coefCaptures[13], markup, message);
            }

            result.HighTemperatureRange = rebasedHighRange;
            result.LowTemperatureRange = lowTempRange;

            return result;
        }

        public override int GetHashCode() => Header.GetHashCode();

        public override string ToString()
        {
            var formatProvider = CultureInfo.InvariantCulture;
            var builder = new StringBuilder(Header.ToString());
            builder.AppendLine();
            string exp = ", 15:0.00000000E+00";
            builder.AppendFormat(formatProvider,
                @"{0$}{1$}{2$}{3$}{4$}{5,5}".Replace("$", exp),
                HighTemperatureRange.A1,
                HighTemperatureRange.A2,
                HighTemperatureRange.A3,
                HighTemperatureRange.A4,
                HighTemperatureRange.A5,
                2);
            builder.AppendLine();
            builder.AppendFormat(formatProvider,
                @"{0$}{1$}{2$}{3$}{4$}{5,5}".Replace("$", exp),
                HighTemperatureRange.A6,
                HighTemperatureRange.A7,
                LowTemperatureRange.A1,
                LowTemperatureRange.A2,
                LowTemperatureRange.A3,
                3);
            builder.AppendLine();
            builder.AppendFormat(formatProvider,
                @"{0$}{1$}{2$}{3$}{4,20}".Replace("$", exp),
                LowTemperatureRange.A4,
                LowTemperatureRange.A5,
                LowTemperatureRange.A6,
                LowTemperatureRange.A7,
                4);
            string result = builder.ToString();
            return result;
        }

        public override bool Equals(object obj)
        {
            var other = obj as NasaA7;
            if (ReferenceEquals(other, null))
                return false;
            var comparer = DoubleEqualityComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 1.0e-18);
            return this.Header.Equals(other.Header)                
                && comparer.Equals(this.LowTemperature, other.LowTemperature)
                && comparer.Equals(this.CommonTemperature, other.CommonTemperature)
                && comparer.Equals(this.HighTemperature, other.HighTemperature)
                && this.LowTemperatureRange.Equals(other.LowTemperatureRange, comparer);
        }        
    }
}
