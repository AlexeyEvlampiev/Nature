using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Nature.Chemkin.Thermo
{
    public class ThermoCollectionTests
    {
        [Fact]
        public void Test1()
        {
            var collection = ThermoCollection.Parse(ThermoCollectionsResource.thermo30);
            Assert.Equal(53, collection.Count);

            foreach (var item in collection.OfType<SpeciesNasaThermoA7Classic>())
            {
                var markup = item.ToString();
                var clonned = SpeciesNasaThermoA7Classic.Parse(markup);
                Assert.Equal(item, clonned);
            }
        }

        [Fact]
        public void Super()
        {
            string baseDir = System.AppContext.BaseDirectory;
            var dirName = Path.Combine(baseDir, "Chemkin", "Mechanisms");
            var mechanisms = new DirectoryInfo(dirName);
            if (!mechanisms.Exists)
                throw new DirectoryNotFoundException(mechanisms.FullName);
            var thermoFiles = mechanisms.GetFiles("*therm*", SearchOption.AllDirectories);
            foreach (var tf in thermoFiles)
            {
                string text = File.ReadAllText(tf.FullName);
                RunTests(text);
            }
        }

        void RunTests(string text)
        {
            var collection = ThermoCollection.Parse(text);
            
            foreach (var item in collection.OfType<SpeciesNasaThermoA7Classic>())
            {
                var markup = item.ToString();
                var clonned = SpeciesNasaThermoA7Classic.Parse(markup);
                Assert.Equal(item, clonned);
            }
        }
    }
}
