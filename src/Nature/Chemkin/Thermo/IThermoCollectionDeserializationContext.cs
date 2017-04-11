using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Chemkin.Thermo
{
    public interface IThermoCollectionDeserializationContext : 
        IDeserializationContext,
        INasaA7DeserializationContext
    {
    }
}
