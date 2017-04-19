namespace Nature.Common
{
    using System;
    using System.Collections.Generic;

    public sealed class ChemicalElementMassRepository
        :  Dictionary<string, double>, IChemicalElementMassRepository
    {
        public ChemicalElementMassRepository() 
            : base(StringComparer.OrdinalIgnoreCase)
        {
            foreach (var el in ChemicalElementInfo.GetChemicalElements())
            {
                this.Add(el.Code, el.Mass);
            }
        }        

        double IChemicalElementMassRepository.GetMolarMass(string elementCode)
        {
            try
            {
                return this[elementCode];
            }
            catch (KeyNotFoundException)
            {
                throw new ChemicalElementDataNotFoundException(elementCode);
            }
            
        }

        bool IChemicalElementMassRepository.HasMass(string elementCode)
        {
            return this.ContainsKey(elementCode);
        }
    }
}
