using Xunit;

namespace Nature.Chemkin.Thermo
{
    public class SpeciesNasaThermoHeaderFormatOptions_ctro_Should
    {
        [Fact]
        public void Work()
        {
            var options = new SpeciesNasaThermoHeaderFormatOptions();
            Assert.Equal(18, options.SpeciesIdWidth);
            Assert.Equal(6, options.DateWidth);
            Assert.Equal(5, options.ElementWidth);
            Assert.Equal(4, options.ElementsMaxCount);
        }
    }
}
