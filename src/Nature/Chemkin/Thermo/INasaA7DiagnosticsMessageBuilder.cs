using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Chemkin.Thermo
{
    public interface INasaA7DiagnosticsMessageBuilder
    {
        string InvalidNasaA7HeaderFormat();
        string SpeciesCodeIsMissing();
        string UnexpectedInputInSpeciesCodeArea(string defect);
        string InvalidPhaseIdentifier(string value);
    }
}
