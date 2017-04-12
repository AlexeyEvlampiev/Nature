namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Xunit;

    public class ThermoCollectionTests
    {

        [Fact]
        public void GRI3()
        {
            var collection = ThermoCollection.Parse(ThermoCollectionsResource.thermo30);
            Assert.Equal(53, collection.Count);

            foreach (var item in collection.OfType<NasaA7>())
            {
                var markup = item.ToString();
                var clonned = NasaA7.Parse(markup);
                Assert.Equal(item, clonned);
            }

            var ch4 = collection.FirstOrDefault<ISpeciesThermo>("CH4");
            double dhf = ch4.H(298.15);
        }




        [Fact]
        public void SanDiego()
        {
            var collection = ThermoCollection.Parse(ThermoCollectionsResource.sandiego20160815_therm);
            Assert.Equal(80, collection.Count);

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
                catch (ChemkinException ex)
                {
                    if (tf.Name.Equals("chx_ver1h_therm.txt", StringComparison.OrdinalIgnoreCase)
                        && ex.AllMessages.Count == 1
                        && ex.AllMessages[0].Contains("5355"))
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    else if (tf.Name.Equals("gasoline_surrogate_therm.dat.txt", StringComparison.OrdinalIgnoreCase)
                        && ex.AllMessages.Count == 1
                        && ex.AllMessages[0].Contains("5806"))
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                    
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
