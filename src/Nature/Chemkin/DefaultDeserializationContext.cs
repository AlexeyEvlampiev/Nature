namespace Nature.Chemkin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using Nature.Common;    

    public class DefaultDeserializationContext : 
        Thermo.INasaA7HeaderDeserializationContext,
        Thermo.INasaA7DeserializationContext,
        Thermo.IThermoCollectionDeserializationContext
    {
        readonly Stack<object> _services = new Stack<object>();

        public DefaultDeserializationContext()
        {
            _services.Push(new DebugDiagnosticsCallback());
            _services.Push(new Thermo.NasaA7HeaderFormatOptions());
            _services.Push(new DefaultDeserializationSession());
            _services.Push(new DefaultFormatInfo());
            _services.Push(new DefaultMessageBuilder());
            _services.Push(new DefaultSpeciesThermodynamicFunctionsValidator());
        }

        [DebuggerStepThrough]
        public T First<T>() => _services.OfType<T>().First();

        [DebuggerStepThrough]
        public T FirstOrDefault<T>() => _services.OfType<T>().FirstOrDefault();

        public double? DefaultLowTemperature { get; set;}

        public double? DefaultHighTemperature { get; set; }

        public double? DefaultCommonTemperature { get; set; }

        public IDeserializationDiagnosticsCallack GetDiagnosticsCallback() => First<IDeserializationDiagnosticsCallack>();

        Thermo.INasaA7HeaderFormatOptions Thermo.INasaA7HeaderDeserializationContext.GetOptions() => First<Thermo.INasaA7HeaderFormatOptions>();

        public IDeserializationSession GetSession() => First<IDeserializationSession>();

        public IFormatInfo GetFormatInfo() => First<IFormatInfo>();

        Thermo.INasaA7DiagnosticsMessageBuilder Thermo.INasaA7HeaderDeserializationContext.GetMessageBuilder() => First<Thermo.INasaA7DiagnosticsMessageBuilder>();

        [DebuggerStepThrough]
        public void Push(object obj)
        {
            _services.Push(obj);
        }

        public ISpeciesThermodynamicFunctionsValidator
            GetSpeciesThermodynamicFunctionsValidator() => First<ISpeciesThermodynamicFunctionsValidator>();
    }
}
