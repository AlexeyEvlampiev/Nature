namespace Nature.Common
{
    public interface ISpeciesThermo : IChemicalSpeciesInfo
    {
        double ReducedCp(double temperature);
        double ReducedH(double temperature);
        double ReducedS(double temperature);
    }
}
