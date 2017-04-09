namespace Nature.Chemkin.Thermo
{
    using Nature.Chemkin.Common;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public sealed class SpeciesNasaThermoA7Classic : SpeciesNasaThermoA7
    {
        private SpeciesNasaThermoHeader _header;
        public override SpeciesNasaThermoHeader Header => _header;

        public NasaA7Approximation LowTemperatureApproximation { get; private set; }

        public NasaA7Approximation HighTemperatureApproximation { get; private set; }

        public static SpeciesNasaThermoA7Classic Parse(
            string text,
            ISpeciesNasaThermoA7ClassicDeserializationContext context = null)
        {
            ChemkinMarkup markup = new ChemkinMarkup(text);
            context = context ?? new DefaultDeserializationContext();
            return Parse(markup, context, null);
        }

        internal static SpeciesNasaThermoA7Classic Parse(
            ChemkinMarkup markup,
            ISpeciesNasaThermoA7ClassicDeserializationContext context,
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

            string urlParams = SpeciesNasaThermoHeaderFormatOptions.BuildUrlParams(options);
            var regex = session.GetOrCreate<Regex>($"nasa-a7-classic/regex?{urlParams}", 
                ()=> 
                {
                    int width = options.SpeciesIdWidth +
                    options.DateWidth +
                    options.ElementsMaxCount * options.ElementWidth + 35;
                    string pattern = $@"
                    ^(?<header>.{{{width}}} [1] .* ) \n
                    ^(?<a>.{{15}}){{5}} .{{4}}  [2] .* \n
                    ^(?<a>.{{15}}){{5}} .{{4}}  [3] .* \n
                    ^(?<a>.{{15}}){{4}} .{{19}} [4]";
                    pattern = RegexUtils.Minify(pattern);
                    return new Regex(pattern, RegexOptions.Multiline);
                });

            var result = new SpeciesNasaThermoA7Classic();
            var match = regex.Match(markup.AdaptedText, index, length);
            result._header = SpeciesNasaThermoHeader.Parse(markup, context, match.Groups["header"]);

            var highTempRange = new double[7];
            var lowTempRange = new double[7];
            var coefCaptures = match.Groups["a"].Captures;
            for (int i = 0; i < 14; ++i)
            {
                var coefCapture = coefCaptures[i];
                int range = i / 7;
                int ordinal = i % 7;
                if (formatInfo.IsRealNumber(coefCapture.Value))
                {
                    var value = formatInfo.ToDouble(coefCapture.Value);
                    var temp = range == 0 ? highTempRange : lowTempRange;
                    temp[ordinal] = value;
                }
            }

            result.HighTemperatureApproximation = new NasaA7Approximation(highTempRange);
            result.LowTemperatureApproximation = new NasaA7Approximation(lowTempRange);
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
                HighTemperatureApproximation.A1,
                HighTemperatureApproximation.A2,
                HighTemperatureApproximation.A3,
                HighTemperatureApproximation.A4,
                HighTemperatureApproximation.A5,
                2);
            builder.AppendLine();
            builder.AppendFormat(formatProvider,
                @"{0$}{1$}{2$}{3$}{4$}{5,5}".Replace("$", exp),
                HighTemperatureApproximation.A6,
                HighTemperatureApproximation.A7,
                LowTemperatureApproximation.A1,
                LowTemperatureApproximation.A2,
                LowTemperatureApproximation.A3,
                3);
            builder.AppendLine();
            builder.AppendFormat(formatProvider,
                @"{0$}{1$}{2$}{3$}{4,20}".Replace("$", exp),
                LowTemperatureApproximation.A4,
                LowTemperatureApproximation.A5,
                LowTemperatureApproximation.A6,
                LowTemperatureApproximation.A7,
                4);
            string result = builder.ToString();
            return result;
        }

        public override bool Equals(object obj)
        {
            var other = obj as SpeciesNasaThermoA7Classic;
            if (ReferenceEquals(other, null))
                return false;
            return this.Header.Equals(other.Header)
                && this.HighTemperatureApproximation.Equals(other.HighTemperatureApproximation)
                && this.LowTemperatureApproximation.Equals(other.LowTemperatureApproximation);
        }
    }
}
