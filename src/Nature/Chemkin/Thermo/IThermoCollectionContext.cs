namespace Nature.Chemkin.Thermo
{
    public interface IThermoCollectionContext
    {
        double? DefaultLowTemperature { get; }
        double? DefaultCommonTemperature { get; }
        double? DefaultHighTemperature { get; }
    }
}
