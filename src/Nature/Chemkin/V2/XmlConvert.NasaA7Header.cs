namespace Nature.Chemkin.V2
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Text.RegularExpressions;

    public partial class XmlConvert
    {
        public XElement NasaA7Header(ChemkinMarkup markup, Capture capture)
        {
            var xheader = XElement("species-info", capture, markup);
            Regex regex = GetOrCreate<Regex>("chemkin/regex/nasa-a7/header", () =>
            {
                var options = _nasaA7HeaderFormatOptions;
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
            var match = regex.Match(markup.AdaptedText, capture);            
            xheader.Add(NasaA7HeaderCode(markup, match.Groups["code"]));
            xheader.Add(NasaA7HeaderDate(markup, match.Groups["date"]));
            xheader.Add(NasaA7HeaderPhase(markup, match.Groups["phase"]));            

            foreach (Capture c in match.Groups["element"].Captures)
            {
                xheader.Add(NasaA7HeaderElements(markup, c));
            }
            return xheader;
        }

        private XElement NasaA7HeaderElements(ChemkinMarkup markup, Capture capture)
        {
            
            var regex = GetOrCreate<Regex>("chemkin/regex/nasa-a7/header/element", () =>
            {
                var options = _nasaA7HeaderFormatOptions;
                var pattern = $@"(?<code>.{{2}})(?<content>.{{{options.ElementWidth - 2}}})";
                pattern = RegexUtils.Minify(pattern);
                return new Regex(pattern);
            });
            var emptyElementRegex = GetOrCreate<Regex>("chemkin/regex/nasa-a7/header/element/empty", () => new Regex(@"^[0\s]+$"));

            if (emptyElementRegex.IsMatch(capture.Value))
                return null;
            var m = regex.Match(markup.AdaptedText, capture);
            var elementCodeCapture = m.Groups["code"];
            var elementContentCapture = m.Groups["content"];
            var elementCode = elementCodeCapture.Value.Trim();
            var content = _formatInfo.ToDouble(elementContentCapture.Value);
            return XElement("element", capture, markup,
                XElement("element-id", elementCodeCapture, markup, new XAttribute("value", elementCode)),
                XElement("amount", elementContentCapture, markup,new XAttribute("value", content)));
        }

        private XElement NasaA7HeaderPhase(ChemkinMarkup markup, Group capture)
        {
            return XElement("phase", capture, markup, new XAttribute("value", capture));
        }

        private XElement NasaA7HeaderDate(ChemkinMarkup markup, Capture capture)
        {
            return XElement("date", capture, markup, new XAttribute("value", capture));
        }

        private XElement NasaA7HeaderCode(ChemkinMarkup markup, Capture capture)
        {
            var regex = GetOrCreate<Regex>("chemkin/regex/nasa-a7/header/code", () =>
                new Regex(RegexUtils.Minify(@"(?<code>\S+)\s*(?<defect>\S.*\S)?")));
            var match = regex.Match(markup.AdaptedText, capture);
            var codeCapture = match.Groups["code"];
            return XElement("species-id", codeCapture, markup, new XAttribute("value", codeCapture));
        }
    }
}
