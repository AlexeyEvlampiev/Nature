﻿using Xunit;

namespace Nature.Chemkin.Thermo
{
    public class NasaA7_ToString_Should
    {
        [Fact]
        public void Work()
        {
            var expected = NasaA7.Parse(@"
CH2CHO            SAND86O   1H   3C   2     G   300.000  5000.000  1000.000    1
 0.05975670E+02 0.08130591E-01-0.02743624E-04 0.04070304E-08-0.02176017E-12    2
 0.04903218E+04-0.05045251E+02 0.03409062E+02 0.10738574E-01 0.01891492E-04    3
-0.07158583E-07 0.02867385E-10 0.15214766E+04 0.09558290E+02                   4
");
            var actual = NasaA7.Parse(expected.ToString());
            Assert.Equal(expected, actual);
        }
    }
}
