using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            foreach (var item in collection.OfType<NasaA7>())
            {
                var markup = item.ToString();
                var clonned = NasaA7.Parse(markup);
                Assert.Equal(item, clonned);
            }
        }

        [Fact]
        public void Test2()
        {
            var collection = ThermoCollection.Parse(ThermoCollectionsResource.thermmitsymp2004_dat);
            Assert.Equal(304, collection.Count);

            foreach (var item in collection.OfType<NasaA7>())
            {
                var markup = item.ToString();
                var clonned = NasaA7.Parse(markup);
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
                try
                {
                    string text = File.ReadAllText(tf.FullName);
                    RunTests(text);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(tf.DirectoryName);
                }
            }
        }

        void RunTests(string text)
        {
            var collection = ThermoCollection.Parse(text);
            
            foreach (var item in collection.OfType<NasaA7>())
            {
                var markup = item.ToString();                
                var clonned = NasaA7.Parse(markup);
                Assert.Equal(item, clonned);
            }
        }
    }
}
