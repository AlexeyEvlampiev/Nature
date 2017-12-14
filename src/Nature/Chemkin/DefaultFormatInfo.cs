namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Nature.Common;
    using Text.RegularExpressions;

    public class DefaultFormatInfo : IFormatInfo
    {
        private readonly Regex m_elementIdRegex;
        private readonly Regex m_realNumberRegex;

        private readonly Dictionary<string, ThermodynamicPhase> m_phaseIdentifiers 
            = new Dictionary<string, ThermodynamicPhase>(StringComparer.OrdinalIgnoreCase)
            {
                { "G", ThermodynamicPhase.Gas},
                { "L", ThermodynamicPhase.Liquid },
                { "S", ThermodynamicPhase.Solid }
            };

        public DefaultFormatInfo()
        {
            this.m_elementIdRegex = new Regex(RegexUtils.MinifyStrict(ChemkinPatterns.ElementId));
            this.m_realNumberRegex = new Regex(RegexUtils.MinifyStrict(ChemkinPatterns.SignedRealNumber));
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

        public bool IsValidPhaseIdentifier(string phaseId)
        {
            return this.m_phaseIdentifiers.ContainsKey(phaseId);
        }

        public ThermodynamicPhase ParseThermodynamicPhase(string phaseId)
        {
            try
            {
                return m_phaseIdentifiers[phaseId];
            }
            catch(KeyNotFoundException)
            {
                throw new FormatException();
            }
        }
    }
}
