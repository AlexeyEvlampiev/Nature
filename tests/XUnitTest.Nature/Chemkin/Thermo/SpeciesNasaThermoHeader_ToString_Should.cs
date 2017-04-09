using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nature.Chemkin.Thermo
{
    public class SpeciesNasaThermoHeader_ToString_Should
    {
        [Fact]
        public void Work()
        {
            var expected = SpeciesNasaThermoHeader.Parse(SpeciesNasaThermoHeaderResource.CH3CHO);
            var actual = SpeciesNasaThermoHeader.Parse(expected.ToString());
            Assert.Equal(expected, actual);
        }
    }
}
