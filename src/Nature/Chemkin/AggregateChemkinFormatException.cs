using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nature.Chemkin
{
    public class AggregateChemkinFormatException : FormatException
    {
        internal AggregateChemkinFormatException(IEnumerable<ChemkinFormatException> exceptions) :
            base(string.Join("; " + Environment.NewLine, exceptions.Select(ex => ex.Message)))
        { }
    }
}
