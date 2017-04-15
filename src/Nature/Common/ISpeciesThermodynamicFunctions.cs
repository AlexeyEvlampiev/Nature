namespace Nature.Common
{
    public interface ISpeciesThermodynamicFunctions : IChemicalSpeciesInfo
    {
        double MinTemperature { get; }
        double MaxTemperature { get; }
        double ReducedCp(double temperature);
        double ReducedH(double temperature);
        double ReducedS(double temperature);
    }
}
