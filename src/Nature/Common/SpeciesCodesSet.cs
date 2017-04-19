
namespace Nature.Common
{
    using System;
    using System.Collections.Generic;

    public sealed class SpeciesCodesSet : HashSet<string>
    {
        public SpeciesCodesSet() : base(StringComparer.OrdinalIgnoreCase) { }

        public SpeciesCodesSet(IEnumerable<string> collection) 
            : base(collection, StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
