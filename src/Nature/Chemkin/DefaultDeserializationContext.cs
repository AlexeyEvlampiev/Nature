using System;
using Nature.Chemkin.Thermo;

namespace Nature.Chemkin
{
    class DefaultDeserializationContext : 
        Thermo.INasaA7HeaderDeserializationContext,
        Thermo.INasaA7DeserializationContext,
        Thermo.IThermoCollectionDeserializationContext
    {
        readonly IDeserializationDiagnosticsCallack _diagnosticsCallback = new TraceDiagnosticsCallback();
        readonly Thermo.INasaA7HeaderFormatOptions _chemicalFormulaOptions = new Thermo.NasaA7HeaderFormatOptions();
        readonly IDeserializationSession _session = new DefaultSession();
        readonly IFormatInfo _formatInfo = new DefaultFormatInfo();
        readonly DefaultMessageBuilder _messageBuilder = new DefaultMessageBuilder();

        

        public double? DefaultTmin { get; set;}

        public double? DefaultTmax { get; set; }

        public double? DefaultTcommon { get; set; }

        public IDeserializationDiagnosticsCallack GetDiagnosticsCallback() => _diagnosticsCallback;

        Thermo.INasaA7HeaderFormatOptions Thermo.INasaA7HeaderDeserializationContext.GetOptions() => _chemicalFormulaOptions;

        public IDeserializationSession GetSession() => _session;

        public IFormatInfo GetFormatInfo() => _formatInfo;

        Thermo.INasaA7DiagnosticsMessageBuilder Thermo.INasaA7HeaderDeserializationContext.GetMessageBuilder()
        {
            return _messageBuilder;
        }
    }
}
