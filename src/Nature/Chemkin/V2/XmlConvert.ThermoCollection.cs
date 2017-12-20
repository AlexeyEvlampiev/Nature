namespace Nature.Chemkin.V2
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Text.RegularExpressions;

    public partial class XmlConvert
    {
        private XElement ThermoCollection(ChemkinMarkup markup, Capture capture)
        {
            var xcollection = XElement("themo-collection", capture, markup);
            var regex = GetOrCreate<Regex>("chemkin/regex/thermo-collection/", () =>
            {
                string pattern = @"
                    ^s* \b THERMO? \b .* \n
                    (?>^ \s* (?<defaultTemperatures> @number (?:\s* @number ){2}  \s* \n )    )?
                    (?<body>
                        (?> ^(?!\s*\bEND\b) .+\n?)*
                    )?
                    (^\s* \b END \b \s* )?
                    ".Replace("@number", "(?<temp>@number)")
                    .Replace("@number", ChemkinPatterns.SignedRealNumber);
                pattern = RegexUtils.Minify(pattern);
                return new Regex(pattern, RegexOptions.Multiline);
            });

            var match = regex.Match(markup.AdaptedText, capture.Index, capture.Length);
            var defaultTemperaturesCapture = match.Groups["defaultTemperatures"];
            var bodyCapture = match.Groups["body"];
            if (defaultTemperaturesCapture.Success)
            {
                var xdefaultTemperatures = XElement("default-temperatures", defaultTemperaturesCapture, markup);
                xcollection.Add(xdefaultTemperatures);
                var xtemp = match.Groups["temp"].Captures
                    .OrderBy(c=> _formatInfo.ToDouble(c.Value))
                    .ToList();
                xdefaultTemperatures.Add(XElement("tmin", xtemp[0], markup, new XAttribute("value", xtemp[0])));
                xdefaultTemperatures.Add(XElement("tcom", xtemp[1], markup, new XAttribute("value", xtemp[1])));
                xdefaultTemperatures.Add(XElement("tmax", xtemp[2], markup, new XAttribute("value", xtemp[2])));
            }

            regex = GetOrCreate<Regex>("chemkin/regex/thermo-collection/body", () =>
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

            for (match = regex.Match(markup.AdaptedText, bodyCapture); match.Success; match = match.NextMatch())
            {
                xcollection.Add(NasaA7(markup, match));
            }

            return xcollection;
        }
    }
}
