namespace Nature.Common
{
    using System;

    public sealed class ChemicalElementDataNotFoundException : Exception
    {
        public ChemicalElementDataNotFoundException(string elementCode) 
            : base($"'{elementCode}' data not found.")
        {
        }
    }
}
