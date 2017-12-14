using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Chemkin
{
    using System.Collections;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Text;
    using Text.RegularExpressions;

    public class ChemkinModule : IEnumerable
    {
        private readonly List<IEnumerable> _moduleCollections = new List<IEnumerable>();

        private ChemkinModule()
        {
        }

        [DebuggerStepThrough]
        public static ChemkinModule Parse(string markup)
        {
            return Parse(new ChemkinMarkup(markup), new DeserializationContext(), new DebugDiagnosticsCallback());
        }

        public static ChemkinModule Parse(
            ChemkinMarkup markup,
            DeserializationContext context,
            IDeserializationDiagnosticsCallback diagnosticsCallack)
        {
            if (markup == null) throw new ArgumentNullException(nameof(markup));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (diagnosticsCallack == null) throw new ArgumentNullException(nameof(diagnosticsCallack));

            int index = 0;
            int length = markup.AdaptedText.Length;
            var module = new ChemkinModule();
            using (context.Push(module))
            {
                Debug.Assert(context.FistOrDefault<ChemkinModule>() == module);
                Parse(markup, context, diagnosticsCallack, index, length, module);                
            }          
            Debug.Assert(context.FistOrDefault<ChemkinModule>() != module);
            return module;
        }

        private static void Parse(ChemkinMarkup markup, DeserializationContext context,
            IDeserializationDiagnosticsCallback diagnosticsCallack, int index, int length, ChemkinModule module)
        {
            var regex = context.GetOrCreate<Regex>("chemkin/regex/module", () =>
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

            Match match = regex.Match(markup.AdaptedText, index, length);
            for (; match.Success; match = match.NextMatch())
            {
                var openTag = match.Groups["open"].Value;
                if (openTag.StartsWith("THERM"))
                {
                    var thermoCollection =
                        Thermo.ThermoCollection.Parse(markup, match, context, diagnosticsCallack);
                    module._moduleCollections.Add(thermoCollection);
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var moduleCollection in _moduleCollections)
            {
                foreach (object item in moduleCollection)
                {
                    yield return item;
                }
            }
        }       
    }
}
