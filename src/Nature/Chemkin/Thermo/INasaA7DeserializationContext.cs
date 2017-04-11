using System;

namespace Nature.Chemkin.Thermo
{
    public interface INasaA7DeserializationContext : 
        IDeserializationContext,
        INasaA7HeaderDeserializationContext
    {
        double? DefaultTmin { get; set; }
        double? DefaultTmax { get; set; }
        double? DefaultTcommon { get; set; }

    }
}
