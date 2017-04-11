using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Chemkin.Thermo
{
    public interface INasaA7HeaderDeserializationContext : IDeserializationContext
    {
        INasaA7HeaderFormatOptions GetOptions();

        INasaA7DiagnosticsMessageBuilder GetMessageBuilder();
    }
}
