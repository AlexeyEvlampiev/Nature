namespace Nature.Common
{
    public interface ISpeciesThermodynamicFunctions : IChemicalSpeciesInfo
    {
        double ReducedCp(double temperature);
        double ReducedH(double temperature);
        double ReducedS(double temperature);
    }
}
