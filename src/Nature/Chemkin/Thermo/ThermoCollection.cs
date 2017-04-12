using Nature.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nature.Chemkin.Thermo
{
    public sealed class ThermoCollection : IReadOnlyList<object>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        readonly List<object> _items = new List<object>();

        public object this[int index] => _items[index];

        public int Count => _items.Count;

        public T FirstOrDefault<T>(string speciesCode) where T : IChemicalSpeciesInfo
        {
            return _items.OfType<T>()
                .FirstOrDefault(sp => sp.SpeciesCode.Equals(speciesCode, StringComparison.OrdinalIgnoreCase));
        }

        public static ThermoCollection Parse(
            string text,
            IThermoCollectionDeserializationContext context = null)
        {
            var markup = new ChemkinMarkup(text);
            context = context ?? new DefaultDeserializationContext();
            return Parse(new ChemkinMarkup(text), context, null);
        }

        internal static ThermoCollection Parse(
            ChemkinMarkup markup,
            IThermoCollectionDeserializationContext context,
            Capture capture)
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
            //var options = context.GetOptions();
            var diagnosticsCallback = context.GetDiagnosticsCallback();

            var regex = session.GetOrCreate<Regex>("thermo-collection/regex", 
                ()=> 
                {
                    string pattern = @"
                    ^s* \b THERMO? \b .* \n
                    (?:^
                        \s*
                        (?<defaultTemperatures>
                           @number (\s* @number ){2}  \s* \n
                        )
                    )?
                    (?<body>
                        (?: ^(?!\s*\bEND\b) .+\n?)*
                    )?
                    (^\s* \b END \b \s* )?
                    ".Replace("@number", "(?<temp>@number)")
                    .Replace("@number", ChemkinPatterns.SignedRealNumber);
                    pattern = RegexUtils.Minify(pattern);
                    return new Regex(pattern, RegexOptions.Multiline);
                });

            context.DefaultCommonTemperature = context.DefaultHighTemperature = context.DefaultLowTemperature = null;
            var formatInfo = context.GetFormatInfo();
            var match = regex.Match(markup.AdaptedText, index, length);
            var defaultTemperaturesGroup = match.Groups["temp"];
            if (defaultTemperaturesGroup.Success)
            {
                double[] temperatures = new double[3];
                var tempCaptures = defaultTemperaturesGroup.Captures;
                for (int i = 0; i < tempCaptures.Count; ++i)
                {
                    Capture c = tempCaptures[i];
                    double value = formatInfo.ToDouble(c.Value);
                    temperatures[i] = value;
                }
                Array.Sort(temperatures);
                context.DefaultLowTemperature = temperatures[0];
                context.DefaultCommonTemperature = temperatures[1];
                context.DefaultHighTemperature = temperatures[2];
            }

            var body = match.Groups["body"];

            var bodyRegex = session.GetOrCreate<Regex>("thermo-collection-body/regex",
                ()=> 
                {
                    string pattern = @"
                    (?: (?=\s*\S)
                        ^.*\n?
                        (?: ^(?!.{79}1) .*\n?  )*
                    )
                    ";
                    pattern = RegexUtils.Minify(pattern);
                    return new Regex(pattern, RegexOptions.Multiline);
                });

            var collection = new ThermoCollection();
            var exceptions = new List<ChemkinException>();
            for (Match itemMatch = bodyRegex.Match(markup.AdaptedText, body.Index, body.Length);
                itemMatch.Success;
                itemMatch = itemMatch.NextMatch())
            {
                try
                {
                    var classic = NasaA7.Parse(markup, context, itemMatch);
                    collection._items.Add(classic);
                }
                catch (ChemkinException ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw ChemkinException.Aggregate(exceptions);
            }

            return collection;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return ((IReadOnlyList<object>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IReadOnlyList<object>)_items).GetEnumerator();
        }
    }

}
