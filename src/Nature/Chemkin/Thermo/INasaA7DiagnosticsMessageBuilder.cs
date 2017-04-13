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
        string MissingInputEolField(int relativeLineNumber);
        string InvalidNasaA7Format();
        string MissingHighTemperature();
        string MissingLowTemperature();
        string MissingCommonTemperature();
        string CpDiscontinuity(string speciesCode, double expectedA1, double actualA1);
        string EnthalpyDiscontinuity(string speciesCode, double expectedA6, double actualA6);
        string EntropyDiscontinuity(string speciesCode, double expectedA7, double actualA7);
    }
}
