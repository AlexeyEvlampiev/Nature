namespace Nature.Common
{
    public static class SpeciesThermodynamicFunctionsExtensions
    {
        public static double H(this ISpeciesThermodynamicFunctions self, double temperature) => MolarEnthalpy(self, temperature);

        public static double S(this ISpeciesThermodynamicFunctions self, double temperature) => MolarEntropy(self, temperature);

        public static double Cp(this ISpeciesThermodynamicFunctions self, double temperature) => MolarCp(self, temperature);

        public static double MolarEnthalpy(this ISpeciesThermodynamicFunctions self, double temperature)
        {
            return temperature * Constants.Rgas * self.ReducedH(temperature);
        }

        public static double MolarCp(this ISpeciesThermodynamicFunctions self, double temperature)
        {
            return Constants.Rgas * self.ReducedCp(temperature);
        }

        public static double StandardMolarEnthalpyChangeOfFormation(this ISpeciesThermodynamicFunctions self) => MolarEnthalpy(self, Constants.StandardStateTemperature);

        public static double MolarEntropy(this ISpeciesThermodynamicFunctions self, double temperature)
        {
            return Constants.Rgas * self.ReducedS(temperature);
        }

        public static double StandardMolarEntropy(this ISpeciesThermodynamicFunctions self) => MolarEntropy(self, Constants.StandardStateTemperature);

        public static double StandardHeatCapacityAtConstantPressure(this ISpeciesThermodynamicFunctions self) => MolarCp(self, Constants.StandardStateTemperature);
    }
}
