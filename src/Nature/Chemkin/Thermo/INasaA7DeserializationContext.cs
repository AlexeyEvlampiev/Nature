namespace Nature.Chemkin.Thermo
{
    using Nature.Common;

    public interface INasaA7DeserializationContext : 
        IDeserializationContext,
        INasaA7HeaderDeserializationContext
    {
        ISpeciesThermodynamicFunctionsValidator GetSpeciesThermodynamicFunctionsValidator();

    }
}
