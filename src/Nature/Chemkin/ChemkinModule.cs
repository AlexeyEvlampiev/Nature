namespace Nature.Chemkin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using Text;
    using Text.RegularExpressions;

    public class ChemkinModule : IEnumerable<object>
    {
        private readonly List<IEnumerable> _moduleCollections = new List<IEnumerable>();

        private ChemkinModule()
        {
        }

        [DebuggerStepThrough]
        public static ChemkinModule Parse(string markup)
        {
            return Parse(new ChemkinMarkup(markup), new DeserializationContext());
        }

        public static ChemkinModule Parse(
            ChemkinMarkup markup,
            DeserializationContext context)
        {
            if (markup == null) throw new ArgumentNullException(nameof(markup));
            if (context == null) throw new ArgumentNullException(nameof(context));

            var parser = new ChemkinModuleParser(markup, context);
            return parser.Result;
        }


        public IEnumerator<object> GetEnumerator()
        {
            foreach (var moduleCollection in _moduleCollections)
            {
                foreach (object item in moduleCollection)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        class ChemkinModuleParser : ChemkinParser<ChemkinModule>
        {
            private Match _collectionMatch;
            private ChemkinModule _instance;

            public ChemkinModuleParser(ChemkinMarkup markup, DeserializationContext context)
                : base(markup, context)
            {
            }

            protected override ChemkinModule CreateInstance() => new ChemkinModule();

            protected override void Parse(ChemkinModule instance)
            {
                _instance = instance;
                var regex = Context.GetOrCreate<Regex>("chemkin/regex/module", () =>
                {
                    string pattern = @"(?=\S)
                    (?<open>\b(?:@elements|@species|@thermo|@unknown)\b)
                    (?<content >(?> (?!=\b(@element|@species|@thermo|END)\b).)+ )?
                    (?<close>\bEND\b)?    
                    ".Replace("@elements", "ELEM(?:ENTS)?")
                        .Replace("@species", "SPEC(?:IES)?")
                        .Replace("@thermo", "THERMO?")
                        .Replace("@unknown", @"\S{1,20}");
                    pattern = RegexUtils.Minify(pattern);
                    return new Regex(pattern, RegexOptions.Singleline);
                });

                var exceptions = new ChemkinExceptionCollection();
                _collectionMatch = regex.Match(Markup.AdaptedText, 0, Markup.AdaptedText.Length);
                for (; _collectionMatch.Success; _collectionMatch = _collectionMatch.NextMatch())
                {                  
                    exceptions.TryCatch(ParseCollection);
                }
                exceptions.ThrowIfNotEmpty();
            }

            private void ParseCollection()
            {
                var openTag = _collectionMatch.Groups["open"].Value;
                if (openTag.StartsWith("THERM"))
                {
                    var thermoCollection =
                        Thermo.ThermoCollection.Parse(Markup, _collectionMatch, Context, DiagnosticsCallback);
                    _instance._moduleCollections.Add(thermoCollection);
                }
            }
        }
    }
}
