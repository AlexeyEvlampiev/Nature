namespace Nature.Chemkin.Thermo
{
    using Xunit;

    public class ThermoCollection_Parse_Should
    {
        [Fact]
        public void Work()
        {
            var collection = ThermoCollection.Parse(ThermoCollectionsResource.thermo30);
            Assert.Equal(collection.Count, 53);
        }
    }
}
