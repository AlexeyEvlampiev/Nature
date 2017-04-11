namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class NasaA7Base : IChemicalFormula
    {
        public abstract NasaA7Header Header { get; }

        public IEnumerable<string> ElementCodes => Header.ElementCodes;

        public double GetElementContent(string elementCode) => Header.GetElementContent(elementCode);
    }
}
