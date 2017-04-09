namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public sealed class SpeciesNasaThermoHeader : IChemicalFormula
    {        
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        readonly Dictionary<string, double> _formula = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        private SpeciesNasaThermoHeader() { }

        public string SpeciesCode { get; private set; }
        public string Tag { get; private set; }
        public char Phase { get; private set; }

        public double? Tmin { get; private set; }
        public double? Tmax { get; private set; }
        public double? Tcommon { get; private set; }

        public IEnumerable<string> ElementCodes => _formula.Keys;

        public double GetElementContent(string elementCode) => _formula.ContainsKey(elementCode) ? _formula[elementCode] : default(double);

        public double this[string elementCode] => GetElementContent(elementCode);

        public static SpeciesNasaThermoHeader Parse(string text, ISpeciesNasaThermoHeaderDeserializationContext context = null)
        {
            if (ReferenceEquals(text, null))
                throw new ArgumentNullException(nameof(text));
            var markup = new ChemkinMarkup(text);
            context = context ?? new DefaultDeserializationContext();
            return Parse(markup, context);
        }

        internal static SpeciesNasaThermoHeader Parse(
            ChemkinMarkup markup,
            ISpeciesNasaThermoHeaderDeserializationContext context,
            Capture capture = null)
        {
            if (ReferenceEquals(markup, null))
                throw new ArgumentNullException(nameof(markup));
            if (ReferenceEquals(context, null))
                throw new ArgumentNullException(nameof(context));


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

            string urlParams = SpeciesNasaThermoHeaderFormatOptions.BuildUrlParams(options);
            string sessionKey = $"chemical-formula/regex?{urlParams}";
            Regex r = session.GetOrCreate<Regex>(sessionKey, () => {
                var pattern = $@"
                    (?<code>.{{{options.SpeciesIdWidth}}})
                    (?<date>.{{{options.DateWidth}}})
                    (?<element>.{{{options.ElementWidth}}}){{{options.ElementsMaxCount}}}
                    (?<phase>.)
                    (?<tmin>.{{10}})
                    (?<tmax>.{{10}})
                    (?<tcom>.{{8}})
                    (?<element>.{{5}})
                    .1\s*
                    (?<formula>\S.*)?";
                pattern = RegexUtils.Minify(pattern);
                return new Regex(pattern, RegexOptions.Multiline);
            });

            var header = new SpeciesNasaThermoHeader();

            var match = r.Match(markup.AdaptedText, index, length);
            if (!match.Success)
                throw new FormatException("Could not parse header");
            header.SpeciesCode = match.Groups["code"].Value.Trim();
            header.Tag = match.Groups["date"].Value.Trim();
            header.Phase = match.Groups["phase"].Value[0];
            
            sessionKey = $"chemical-formula/regex/element?{urlParams}";
            r = session.GetOrCreate<Regex>(sessionKey, () => {
                var pattern = $@"(?<code>.{{2}})(?<content>.{{{options.ElementWidth - 2}}})";
                pattern = RegexUtils.Minify(pattern);
                return new Regex(pattern);
            });
            var emptyElementRegex = session.GetOrCreate<Regex>("chemical-formula/empty-element/regex", ()=> new Regex(@"^[0\s]+$"));
            var elementCaptures = match.Groups["element"].Captures;
            for (int i = 0; i < elementCaptures.Count; ++i)
            {
                var c = elementCaptures[i];
                if (emptyElementRegex.IsMatch(c.Value))
                    continue;
                var m = r.Match(markup.AdaptedText, c.Index, c.Length);
                var code = m.Groups["code"].Value.Trim();
                var content = m.Groups["content"].Value.Trim();
                header._formula.Add(code, double.Parse(content));
            }

            if (!string.IsNullOrWhiteSpace(match.Groups["tmin"].Value))
            {
                header.Tmin = double.Parse(match.Groups["tmin"].Value.Trim());
            }
            else if (context.DefaultTmin.HasValue)
            {
                header.Tmin = context.DefaultTmin.Value;
            }
            else
            {
                throw new NotImplementedException();
            }

            if (!string.IsNullOrWhiteSpace(match.Groups["tmax"].Value))
            {
                header.Tmax = double.Parse(match.Groups["tmax"].Value.Trim());
            }
            else if (context.DefaultTmin.HasValue)
            {
                header.Tmax = context.DefaultTmin.Value;
            }
            else
            {
                throw new NotImplementedException();
            }


            if (!string.IsNullOrWhiteSpace(match.Groups["tcom"].Value))
            {
                header.Tcommon = double.Parse(match.Groups["tcom"].Value.Trim());
            }
            else if (context.DefaultTcommon.HasValue)
            {
                header.Tcommon = context.DefaultTcommon.Value;
            }
            else
            {
                throw new NotImplementedException();
            }



            return header;
        }

        public string ToString(ISpeciesNasaThermoHeaderFormatOptions options)
        {
            if (ReferenceEquals(options, null))
                throw new ArgumentNullException(nameof(options));
            var formatProvider = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            
            string format = $"{{0,-{options.SpeciesIdWidth}}}{{1,{options.DateWidth}}}";
            sb.AppendFormat(formatProvider, format, SpeciesCode, Tag);

            if (_formula.Count <= options.ElementsMaxCount 
                && _formula.Values.All(v => v.ToString("g").Length <= (options.ElementWidth - 2)))
            {
                format = $"{{0,-2}}{{1,{options.ElementWidth-2}}}";
                foreach (var pair in _formula)
                {
                    sb.AppendFormat(formatProvider, format, pair.Key, pair.Value);
                }                
            }
            for (int i = 0; i < (options.ElementsMaxCount - _formula.Count); ++i)
                sb.AppendFormat(formatProvider, format, string.Empty, string.Empty);

            sb.Append(Phase);

            format = "{0,10}{1,10}{2,8}";
            sb.AppendFormat(formatProvider, format, Tmin, Tmax, Tcommon);
            sb.AppendFormat(formatProvider, "{0, 6}1", string.Empty);

            var result = sb.ToString();
            return result;
        }

        [DebuggerStepThrough]
        public override string ToString()
        {
            return this.ToString(new SpeciesNasaThermoHeaderFormatOptions());
        }

        public override int GetHashCode() => SpeciesCode.GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as SpeciesNasaThermoHeader;
            if (ReferenceEquals(other, null))
                return false;
            return
                this.SpeciesCode.Equals(other.SpeciesCode, StringComparison.OrdinalIgnoreCase)
                && this.Tag.Equals(other.Tag, StringComparison.OrdinalIgnoreCase)
                && this._formula.Keys.All(code=> other._formula.ContainsKey(code))
                && other._formula.Keys.All(code => this._formula.ContainsKey(code))
                && this._formula.Keys.All(code => other[code] == this[code])
                && this.Phase.Equals(other.Phase);
        }
    }
}
