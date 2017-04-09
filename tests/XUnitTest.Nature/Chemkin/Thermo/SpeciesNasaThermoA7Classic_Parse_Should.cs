using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nature.Chemkin.Thermo
{
    public class SpeciesNasaThermoA7Classic_Parse_Should
    {
        [Fact]
        public void Work()
        {
            var thermo = SpeciesNasaThermoA7Classic.Parse(@"
CH2CHO            SAND86O   1H   3C   2     G   300.000  5000.000  1000.000    1
 0.05975670E+02 0.08130591E-01-0.02743624E-04 0.04070304E-08-0.02176017E-12    2
 0.04903218E+04-0.05045251E+02 0.03409062E+02 0.10738574E-01 0.01891492E-04    3
-0.07158583E-07 0.02867385E-10 0.15214766E+04 0.09558290E+02                   4
");
            Assert.NotNull(thermo.Header);
            Assert.Equal(0.05975670E+02, thermo.HighTemperatureApproximation.A1);
            Assert.Equal(0.08130591E-01, thermo.HighTemperatureApproximation.A2);
            Assert.Equal(-0.02743624E-04, thermo.HighTemperatureApproximation.A3);
            Assert.Equal(0.04070304E-08, thermo.HighTemperatureApproximation.A4);
            Assert.Equal(-0.02176017E-12, thermo.HighTemperatureApproximation.A5);
            Assert.Equal(0.04903218E+04, thermo.HighTemperatureApproximation.A6);
            Assert.Equal(-0.05045251E+02, thermo.HighTemperatureApproximation.A7);

            Assert.Equal(0.03409062E+02, thermo.LowTemperatureApproximation.A1);
            Assert.Equal(0.10738574E-01, thermo.LowTemperatureApproximation.A2);
            Assert.Equal(0.01891492E-04, thermo.LowTemperatureApproximation.A3);
            Assert.Equal(-0.07158583E-07, thermo.LowTemperatureApproximation.A4);
            Assert.Equal(0.02867385E-10, thermo.LowTemperatureApproximation.A5);
            Assert.Equal(0.15214766E+04, thermo.LowTemperatureApproximation.A6);
            Assert.Equal(0.09558290E+02, thermo.LowTemperatureApproximation.A7);
        }
    }
}
