using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Common
{
    public interface IChemicalFormula
    {
        IEnumerable<string> ElementCodes { get; }

        double GetElementContent(string elementCode);
    }
}
