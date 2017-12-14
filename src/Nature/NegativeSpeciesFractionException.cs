using System;

namespace Nature
{
    public abstract class NegativeSpeciesFractionException : Exception
    {
        internal protected NegativeSpeciesFractionException(string speciesCode, int speciesOrdinal, double fraction)
        { }
    }

    public sealed class NegativeSpeciesMassFractionException : NegativeSpeciesFractionException
    {
        public NegativeSpeciesMassFractionException(string speciesCode, int speciesOrdinal, double fraction) : base(speciesCode, speciesOrdinal, fraction)
        {
        }
    }

    public sealed class NegativeSpeciesMoleFractionException : NegativeSpeciesFractionException
    {
        public NegativeSpeciesMoleFractionException(string speciesCode, int speciesOrdinal, double fraction) : base(speciesCode, speciesOrdinal, fraction)
        {
        }
    }
}
