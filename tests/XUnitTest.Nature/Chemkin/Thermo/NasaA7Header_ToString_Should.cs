using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nature.Chemkin.Thermo
{
    public class NasaA7Header_ToString_Should
    {
        [Fact]
        public void Work()
        {
            var expected = NasaA7Header.Parse(NasaA7HeaderResource.CH3CHO);
            var actual = NasaA7Header.Parse(expected.ToString());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Work2()
        {
            var expected = NasaA7Header.Parse(
                @"pofme[oipr]  11/99 glaudp   1o   2h  10c   4g   300.000  5000.000 1386.00f   1 1");
            var actual = NasaA7Header.Parse(expected.ToString());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Work3()
        {
            var expected = NasaA7Header.Parse(
                @"BIN7J      PYRENEJ1     C   0H   0    0    0G   300.000  5000.000 1401.000    01C     1560H      479 ");
            var actual = NasaA7Header.Parse(expected.ToString());
            Assert.Equal(expected, actual);
        }
    }
}
