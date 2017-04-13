namespace Nature.Chemkin
{
    using System;
    using System.Text;

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

        public string CpDiscontinuity(string speciesCode, double expectedA1, double actualA1)
        {
            return new StringBuilder()
                .AppendFormat("Found Cp discontinuity for the '{0}' species. Expected A1: {1, 15:0.00000000E+00}, actual A1: {2, 15:0.00000000E+00},",
                speciesCode, expectedA1, actualA1)
                .ToString();
        }

        public string EnthalpyDiscontinuity(string speciesCode, double expectedA6, double actualA6)
        {
            return new StringBuilder()
                .AppendFormat("Found H discontinuity for the '{0}' species. Expected A6: {1, 15:0.00000000E+00}, actual A6: {2, 15:0.00000000E+00},",
                speciesCode, expectedA6, actualA6)
                .ToString();
        }

        public string EntropyDiscontinuity(string speciesCode, double expectedA7, double actualA7)
        {
            return new StringBuilder()
                .AppendFormat("Found S discontinuity for the '{0}' species. Expected A7: {1, 15:0.00000000E+00}, actual A7: {2, 15:0.00000000E+00},",
                speciesCode, expectedA7, actualA7)
                .ToString();
        }
    }
}
