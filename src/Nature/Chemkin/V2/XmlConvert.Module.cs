namespace Nature.Chemkin.V2
{
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Text;
    using Text.RegularExpressions;
    using Thermo;

    public partial class XmlConvert
    {
        public XElement Module(string ckString)
        {
            var markup = new ChemkinMarkup(ckString);
            var regex = GetOrCreate<Regex>("chemkin/regex/module", () =>
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
                        
            var xmodule = new XElement("module");
            for (Match match = regex.Match(markup.AdaptedText); match.Success; match = match.NextMatch())
            {                
                switch (match.Groups["open"].Value)
                {
                    case ("THERM"):
                    case ("THERMO"):
                        xmodule.Add(ThermoCollection(markup, match));
                        break;
                }
            }
            return xmodule;
        }
    }
}
