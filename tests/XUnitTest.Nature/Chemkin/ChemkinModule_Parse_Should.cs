namespace Nature.Chemkin
{
    using System.IO;
    using Thermo;
    using Xunit;

    public class ChemkinModule_Parse_Should
    {
        [Fact]
        public void SucceedWithGRI3Thermo()
        {
            var text = ThermoCollectionsResource.thermo30;
            var module = ChemkinModule.Parse(text);
        }
    }
}
