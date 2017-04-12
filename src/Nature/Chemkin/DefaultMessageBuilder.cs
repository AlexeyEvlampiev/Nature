namespace Nature.Chemkin
{
    using System;

    class DefaultMessageBuilder : Thermo.INasaA7DiagnosticsMessageBuilder
    {
        public string InvalidNasaA7Format() => "Invalid Nasa-A7 format.";

        public string InvalidNasaA7HeaderFormat() => "Invalid format";

        public string InvalidPhaseIdentifier(string value) => $"'{value}' is not a valid phase identifier";

        public string MissingInputEolField(int relativeLineNumber) => $"Expected '{relativeLineNumber}' end-of-line separator is missing.";

        public string MissingHighTemperature() => "The high temperature value could not be determined";

        public string SpeciesCodeIsMissing() => "Species code is missing";

        public string UnexpectedInputInSpeciesCodeArea(string defect) => $"Unexpected additional input in the species code field. Value: '{defect}'";

        public string MissingLowTemperature() => "The low temperature value could not be determined";

        public string MissingCommonTemperature() => "The common temperature value could not be determined";
    }
}
