namespace Nature.Chemkin
{
    using System.Linq;
    using Thermo;
    using Xunit;

    public class ChemkinModule_Parse_Should
    {
        [Fact]
        public void SucceedWithGRI3Thermo()
        {
            var text = ThermoCollectionsResource.thermo30;
            var module = ChemkinModule.Parse(text);
            Assert.Equal(53, module.Count());
        }
    }
}
