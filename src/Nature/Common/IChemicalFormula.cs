namespace Nature.Common
{
    using System.Collections.Generic;

    public interface IChemicalFormula : IChemicalSpeciesInfo
    {
        IEnumerable<string> ElementCodes { get; }

        double GetElementContent(string elementCode);
    }
}
