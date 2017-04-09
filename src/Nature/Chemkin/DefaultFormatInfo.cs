using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Nature.Chemkin
{
    public class DefaultFormatInfo : IFormatInfo
    {
        private readonly Regex m_elementIdRegex;
        private readonly Regex m_realNumberRegex;

        private readonly HashSet<string> m_phaseIdentifiers 
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "G", "L", "S"
            };

        public DefaultFormatInfo()
        {
            this.m_elementIdRegex = new Regex(RegexUtils.MinifyExact(ChemkinPatterns.ElementId));
            this.m_realNumberRegex = new Regex(RegexUtils.MinifyExact(ChemkinPatterns.SignedRealNumber));
        }

        public bool IsValidElementId(string value)
        {
            return this.m_elementIdRegex.IsMatch(value);
        }

        public bool IsRealNumber(string value)
        {
            return this.m_realNumberRegex.IsMatch(value.Trim());
        }

        public double ToDouble(string value)
        {
            return double.Parse(value.Replace("f", "e"), NumberStyles.Any);
        }

        public bool IsElectronId(string id)
        {
            return "E".Equals(id, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsValidPgaseIdentifier(string value)
        {
            return this.m_phaseIdentifiers.Contains(value);
        }
    }
}
