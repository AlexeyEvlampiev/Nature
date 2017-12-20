namespace Nature.Chemkin.V2
{
    using System.IO;
    using Xunit;

    public class XmlConvert_Module_Should
    {
        [Fact]
        public void Work()
        {
            var target = new XmlConvert();
            var xml = target.Module(File.ReadAllText("Chemkin/Mechanisms/gri-3.0/thermo30.dat.txt"));
            Assert.NotNull(xml);
        }
    }
}
