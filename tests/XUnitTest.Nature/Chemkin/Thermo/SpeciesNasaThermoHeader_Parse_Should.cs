namespace Nature.Chemkin.Thermo
{
    using System.Text.RegularExpressions;
    using Xunit;

    public class SpeciesNasaThermoHeader_Parse_Should
    {
        [Fact]
        public void Work()
        {
            var header = SpeciesNasaThermoHeader.Parse(SpeciesNasaThermoHeaderResource.CH3CHO);
            Assert.Equal("CH3CHO", header.SpeciesCode);
            Assert.Equal("L 8/88", header.Tag);
            Assert.Equal(2.0, header["C"]);
            Assert.Equal(4.0, header["H"]);
            Assert.Equal(1.0, header["O"]);


            //var xxx = SpeciesNasaThermoHeader.Parse("CH2CHO            SAND86O   1H   3C   2     G       300      5000    1000      1 ");
        }
    }
}
