namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class DefaultMessageBuilder : Thermo.INasaA7DiagnosticsMessageBuilder
    {
        public string InvalidNasaA7HeaderFormat() => "Invalid format";

        public string InvalidPhaseIdentifier(string value) => $"'{value}' is not a valid phase identifier";

        public string SpeciesCodeIsMissing() => "Species code is missing";

        public string UnexpectedInputInSpeciesCodeArea(string defect) => $"Unexpected additional input in the species code field. Value: '{defect}'";
    }
}
