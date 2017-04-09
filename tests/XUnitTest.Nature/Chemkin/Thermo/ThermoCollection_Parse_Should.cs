namespace Nature.Chemkin.Thermo
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
