namespace Nature.Chemkin.V2
{
    using System;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Text.RegularExpressions;

    public partial class XmlConvert
    {
        private XElement NasaA7(ChemkinMarkup markup, Capture capture)
        {
            var xrecord = XElement("record", capture, markup);           
            var regex = GetOrCreate<Regex>($"chemkin/regex/nasa-a7",() =>
            {
                var options = _nasaA7HeaderFormatOptions;
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


            var match = regex.Match(markup.AdaptedText, capture);
            if (!match.Success)
            {
                throw new NotImplementedException();
            }

            var headerCapture = match.Groups["header"];
            var coefCaptures = match.Groups["a"].Captures;
            xrecord.Add(NasaA7Header(markup, headerCapture));
            return xrecord;
        }
    }
}
