using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Chemkin.Thermo
{
    public interface ISpeciesNasaThermoHeaderDeserializationContext : IDeserializationContext
    {
        ISpeciesNasaThermoHeaderFormatOptions GetOptions();

        double? DefaultTmin { get; }
        double? DefaultTmax { get; }
        double? DefaultTcommon { get; }
    }
}
