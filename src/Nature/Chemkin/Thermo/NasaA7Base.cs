namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System.Collections.Generic;

    public abstract class NasaA7Base : IChemicalFormula, ISpeciesThermodynamicFunctions
    {
        public abstract NasaA7Header Header { get; }

        public string SpeciesCode => Header.SpeciesCode;
        public IEnumerable<string> ElementCodes => Header.ElementCodes;        

        public double GetElementContent(string elementCode) => Header.GetElementContent(elementCode);
        public abstract double ReducedCp(double temperature);
        public abstract double ReducedH(double temperature);
        public abstract double ReducedS(double temperature);
    }
}
