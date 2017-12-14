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
    using Text;
    using Text.RegularExpressions;

    public sealed class NasaA7Header : IChemicalFormula
    {        
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        readonly Dictionary<string, double> _formula = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        private NasaA7Header() { }

        public string SpeciesCode { get; private set; }
        public string Tag { get; private set; }
        public ThermodynamicPhase Phase { get; private set; }

        public double? LowTemperature { get; private set; }
        public double? HighTemperature { get; private set; }
        public double? CommonTemperature { get; private set; }

        public IEnumerable<string> ElementCodes => _formula.Keys;

        public double GetElementContent(string elementCode) => _formula.ContainsKey(elementCode) ? _formula[elementCode] : default(double);

        public double this[string elementCode] => GetElementContent(elementCode);

        public static NasaA7Header Parse(string text, DeserializationContext context = null)
        {
            if (ReferenceEquals(text, null))
                throw new ArgumentNullException(nameof(text));
            var markup = new ChemkinMarkup(text);
            context = context ?? new DeserializationContext();
            return Parse(markup, null, context, new DebugDiagnosticsCallback());
        }

        internal static NasaA7Header Parse(
            ChemkinMarkup markup,            
            Capture capture,
            DeserializationContext context,
            IDeserializationDiagnosticsCallback diagnosticsCallback)
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

            var options = context.GetOrCreate<INasaA7HeaderFormatOptions>(()=> new NasaA7HeaderFormatOptions());
            var formatInfo = context.GetOrCreate<IFormatInfo>(()=> new DefaultFormatInfo());
            var messageBuilder = context.GetOrCreate<INasaA7DiagnosticsMessageBuilder>(()=> new DefaultMessageBuilder());
            var collectionContext = context.GetOrCreate<IThermoCollectionContext>(()=> new DefaultThermoCollectionContext());

            string urlParams = NasaA7HeaderFormatOptions.BuildUrlParams(options);
            string sessionKey = $"chemical-formula/regex?{urlParams}";
            Regex r = context.GetOrCreate<Regex>(sessionKey, () => {
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

            var header = new NasaA7Header();

            var match = r.Match(markup.AdaptedText, index, length);
            if (!match.Success)
            {
                throw new ChemkinFormatException(0, markup, 
                    messageBuilder.InvalidNasaA7HeaderFormat());
            }

            var classicChemicalFormulaGroup = match.Groups["element"];

            var exceptions = new ChemkinExceptionCollection();
            var speciesCodeAreaGroup = match.Groups["code"];
            var speciesCodeRegex = context.GetOrCreate<Regex>("nasa-a7/header/slement-name/regex",()=> 
                new Regex(RegexUtils.Minify(@"(?<code>\S+)\s*(?<defect>\S.*\S)?")));
            var speciesCodeMatch = speciesCodeRegex.Match(markup.AdaptedText, speciesCodeAreaGroup);
            exceptions.TryCatch(()=> {
                if (!speciesCodeMatch.Success)
                {
                    throw new ChemkinFormatException(speciesCodeAreaGroup, markup,
                        messageBuilder.SpeciesCodeIsMissing());
                }
            });
            

            var speciesCodeCapture = speciesCodeMatch.Groups["code"];
            var speciesCodeDefectCapture = speciesCodeMatch.Groups["defect"];
            if (speciesCodeDefectCapture.Success)
            {
                string defect = speciesCodeDefectCapture.Value;
                diagnosticsCallback.Warning(speciesCodeDefectCapture, markup, 
                    messageBuilder.UnexpectedInputInSpeciesCodeArea(defect));
            }

            string speciesCode = speciesCodeCapture.Value.Trim();

            var phaseCapture = match.Groups["phase"];

            header.SpeciesCode = speciesCodeCapture.Value;
            header.Tag = match.Groups["date"].Value.Trim();
            exceptions.TryCatch(()=> 
            {
                if (formatInfo.IsValidPhaseIdentifier(phaseCapture.Value))
                {
                    header.Phase = formatInfo.ParseThermodynamicPhase(phaseCapture.Value);
                }
                else
                {
                    throw new ChemkinFormatException(phaseCapture, markup,
                        messageBuilder.InvalidPhaseIdentifier(header.SpeciesCode, phaseCapture.Value));
                }
            });
            
            
            sessionKey = $"chemical-formula/regex/element?{urlParams}";
            r = context.GetOrCreate<Regex>(sessionKey, () => {
                var pattern = $@"(?<code>.{{2}})(?<content>.{{{options.ElementWidth - 2}}})";
                pattern = RegexUtils.Minify(pattern);
                return new Regex(pattern);
            });
            var emptyElementRegex = context.GetOrCreate<Regex>("chemical-formula/empty-element/regex", ()=> new Regex(@"^[0\s]+$"));
            var elementCaptures = classicChemicalFormulaGroup.Captures;
            for (int i = 0; i < elementCaptures.Count; ++i)
            {
                var c = elementCaptures[i];
                if (emptyElementRegex.IsMatch(c.Value))
                    continue;
                var m = r.Match(markup.AdaptedText, c.Index, c.Length);
                var elementCodeCapture = m.Groups["code"];                
                var elementContentCapture = m.Groups["content"];
                var elementCode = elementCodeCapture.Value.Trim();
                int errorsCount = exceptions.Count;
                exceptions.TryCatch(() => {
                    if (string.IsNullOrWhiteSpace(elementCode))
                    {
                        throw new ChemkinFormatException(elementCodeCapture, markup,
                            messageBuilder.MissingElementCode(speciesCode, i+1));
                    }
                });
                exceptions.TryCatch(()=> {
                    if (!formatInfo.IsRealNumber(elementContentCapture.Value))
                    {
                        throw new ChemkinFormatException(elementContentCapture, markup,
                            messageBuilder.ExpectedRealNumberElementContent(speciesCode, i+1, elementCode, elementContentCapture.Value.Trim()));
                    }
                });

                if (exceptions.Count > errorsCount)
                    continue;

                var content = formatInfo.ToDouble(elementContentCapture.Value);
                if (content == 0.0)
                    continue;
                header._formula.Add(elementCode, content);
            }

            if (!string.IsNullOrWhiteSpace(match.Groups["tmin"].Value))
            {
                header.LowTemperature = formatInfo.ToDouble(match.Groups["tmin"].Value);
            }
            else
            {
                header.LowTemperature = collectionContext.DefaultLowTemperature;
            }

            if (!string.IsNullOrWhiteSpace(match.Groups["tmax"].Value))
            {
                header.HighTemperature = formatInfo.ToDouble(match.Groups["tmax"].Value);
            }
            else
            {
                header.HighTemperature = collectionContext.DefaultHighTemperature;
            }

            if (!string.IsNullOrWhiteSpace(match.Groups["tcom"].Value))
            {
                header.CommonTemperature = formatInfo.ToDouble(match.Groups["tcom"].Value);
            }
            else
            {
                header.CommonTemperature = collectionContext.DefaultCommonTemperature;
            }


            if (match.Groups["formula"].Success)
            {
                var group = match.Groups["formula"];
                var formulaRegex = context.GetOrCreate<Regex>("chemical-formula/extras/regex", () => 
                {
                    string pattern = @"
                        (?>
                            (?>\s*
                            (?> (?<code>\w{1,2}) \s* (?<content>@number) )
                            \s*) |
                            (?<defect>.+ )
                        )*
                    ".Replace("@number", ChemkinPatterns.SignedRealNumber);
                    pattern = RegexUtils.MinifyStrict(pattern);
                    return new Regex(pattern, RegexOptions.Multiline);
                });
                var formulaMatch = formulaRegex.Match(markup.AdaptedText, group.Index, group.Length);
                var codeCaptures = formulaMatch.Groups["code"].Captures;
                var contentCaptures = formulaMatch.Groups["content"].Captures;
                for (int i = 0; i < codeCaptures.Count; ++i)
                {
                    string code = codeCaptures[i].Value;
                    double content = formatInfo.ToDouble(contentCaptures[i].Value);
                    header._formula.Add(code, content);
                }
            }

            exceptions.TryCatch(()=> {
                if (header._formula.Count == 0)
                {
                    string message = messageBuilder.MissingChemicalFormula(header.SpeciesCode);
                    throw new ChemkinFormatException(classicChemicalFormulaGroup.Captures[0], markup, message);
                }
            });

            exceptions.ThrowIfNotEmpty();

            return header;
        }

        public string ToString(INasaA7HeaderFormatOptions options)
        {
            if (ReferenceEquals(options, null))
                throw new ArgumentNullException(nameof(options));
            var formatProvider = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            
            string format = $"{{0,-{options.SpeciesIdWidth}}}{{1,{options.DateWidth}}}";
            sb.AppendFormat(formatProvider, format, SpeciesCode, Tag);

            var printedElements = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var elClassicFormat = $"{{0,-2}}{{1,{options.ElementWidth-2}:g}}";
            foreach (var pair in _formula)
            {
                string el = string.Format(formatProvider, elClassicFormat, pair.Key, pair.Value);
                if (el.Length == options.ElementWidth)
                {
                    sb.Append(el);
                    printedElements.Add(pair.Key);
                    if (printedElements.Count == 4) break;
                }
            }

            for (int i = 0; i < (options.ElementsMaxCount - printedElements.Count); ++i)
                sb.AppendFormat(formatProvider, elClassicFormat , string.Empty, string.Empty);
            
            sb.Append(Phase.ToString().First());

            format = "{0,10}{1,10}{2,8}";
            sb.AppendFormat(formatProvider, format, LowTemperature, HighTemperature, CommonTemperature);
            foreach (var elCode in _formula.Keys.Where(k => !printedElements.Contains(k)))
            {
                var content = _formula[elCode];
                string el = string.Format(formatProvider, elClassicFormat, elCode, content);
                if (el.Length == options.ElementWidth)
                {
                    sb.Append(el);
                    printedElements.Add(elCode);
                    break;
                }
            }

            int space = options.SpeciesIdWidth + options.DateWidth + (1+options.ElementsMaxCount) * options.ElementWidth + 1 + 28 + 1 - sb.Length;
            format = $"{{0,{space}}}1";
            sb.AppendFormat(formatProvider, format, string.Empty);

            foreach (var elCode in _formula.Keys.Where(c => printedElements.Contains(c) == false))
            {
                sb.AppendFormat("{0} {1} ", elCode, _formula[elCode]);
            }

            var result = sb.ToString();
            return result;
        }

        [DebuggerStepThrough]
        public override string ToString()
        {
            return this.ToString(new NasaA7HeaderFormatOptions());
        }

        public override int GetHashCode() => SpeciesCode.GetHashCode();

        public override bool Equals(object obj)
        {
            var other = obj as NasaA7Header;
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
