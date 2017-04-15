namespace Nature.Common
{
    using System;
    public interface ISpeciesThermodynamicFunctionsValidator
    {
        void Validate(
            ISpeciesThermodynamicFunctions target, 
            Action<string> onError,
            Action<string> onWarning = null);
    }
}
