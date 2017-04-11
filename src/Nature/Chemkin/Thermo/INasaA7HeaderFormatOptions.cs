namespace Nature.Chemkin.Thermo
{
    public interface INasaA7HeaderFormatOptions
    {
        int SpeciesIdWidth { get; }
        int DateWidth { get; }

        int ElementWidth { get; }

        int ElementsMaxCount { get; }
    }
}