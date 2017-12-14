using System.Collections.Generic;

namespace Nature.Chemkin.Thermo
{
    public interface INasaA7DiagnosticsMessageBuilder
    {
        string InvalidNasaA7HeaderFormat();
        string SpeciesCodeIsMissing();
        string UnexpectedInputInSpeciesCodeArea(string defect);
        string InvalidPhaseIdentifier(string speciesCode, string phaseIdentifier);
        string MissingInputEolField(int relativeLineNumber);
        string InvalidNasaA7Format();
        string MissingHighTemperature();
        string MissingLowTemperature();
        string MissingCommonTemperature();
        string HeatCapacityDiscontinuity(string speciesCode, double expectedA1, double actualA1);
        string EnthalpyDiscontinuity(string speciesCode, double expectedA6, double actualA6);
        string EntropyDiscontinuity(string speciesCode, double expectedA7, double actualA7);
        string InvalidTemperatureSequence(double? lowTemperature, double? commonTemperature, double? highTemperature);
        string MissingChemicalFormula(string speciesCode);
        string FunctionasValidationFailed(string speciesCode, List<string> errors);
        string MissingElementCode(string speciesCode, int elementIndex);
        string ExpectedRealNumberElementContent(string speciesCode,int elementIndex, string elementCode, string actual);
    }
}
