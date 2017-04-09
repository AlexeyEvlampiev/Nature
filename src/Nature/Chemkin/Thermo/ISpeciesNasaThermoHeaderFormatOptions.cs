namespace Nature.Chemkin.Thermo
{
    public interface ISpeciesNasaThermoHeaderFormatOptions
    {
        int SpeciesIdWidth { get; }
        int DateWidth { get; }

        int ElementWidth { get; }

        int ElementsMaxCount { get; }
    }
}