namespace Nature.Chemkin
{
    class DefaultDeserializationContext : 
        Thermo.ISpeciesNasaThermoHeaderDeserializationContext,
        Thermo.ISpeciesNasaThermoA7ClassicDeserializationContext,
        Thermo.IThermoCollectionDeserializationContext
    {
        readonly IDeserializationDiagnosticsCallack _diagnosticsCallback = new TraceDiagnosticsCallback();
        readonly Thermo.ISpeciesNasaThermoHeaderFormatOptions _chemicalFormulaOptions = new Thermo.SpeciesNasaThermoHeaderFormatOptions();
        readonly IDeserializationSession _session = new DefaultSession();
        readonly IFormatInfo _formatInfo = new DefaultFormatInfo();

        public double? DefaultTmin { get; set;}

        public double? DefaultTmax { get; set; }

        public double? DefaultTcommon { get; set; }

        public IDeserializationDiagnosticsCallack GetDiagnosticsCallback() => _diagnosticsCallback;

        Thermo.ISpeciesNasaThermoHeaderFormatOptions Thermo.ISpeciesNasaThermoHeaderDeserializationContext.GetOptions() => _chemicalFormulaOptions;

        public IDeserializationSession GetSession() => _session;

        public IFormatInfo GetFormatInfo() => _formatInfo;
    }
}
