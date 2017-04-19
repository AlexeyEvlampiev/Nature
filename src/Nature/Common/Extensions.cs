namespace Nature.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;    

    public static class Extensions
    {
        [DebuggerNonUserCode]
        public static Dictionary<string, T>
            ToLookupBySpeciesCode<T>(this IEnumerable<T> self) where T : IChemicalSpeciesInfo
            => self.ToDictionary(i=> i.SpeciesCode, StringComparer.OrdinalIgnoreCase);

        public static double CalculateMolarMass(this IChemicalFormula self, IChemicalElementMassRepository repository)
        {
            double mass = 0.0;
            foreach (var elementCode in self.ElementCodes)
            {
                double elementContent = self.GetElementContent(elementCode);
                double elementMass = repository.GetMolarMass(elementCode);
                mass += elementContent * elementMass;
            }
            return mass;
        }
    }
}
