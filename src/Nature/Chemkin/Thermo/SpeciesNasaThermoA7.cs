namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class SpeciesNasaThermoA7 : IChemicalFormula
    {
        public abstract SpeciesNasaThermoHeader Header { get; }

        public IEnumerable<string> ElementCodes => Header.ElementCodes;

        public double GetElementContent(string elementCode) => Header.GetElementContent(elementCode);
    }
}
