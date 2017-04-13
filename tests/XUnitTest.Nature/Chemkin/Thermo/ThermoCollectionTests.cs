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

            var comparer = DoubleEqualityComparer.FromRelativeTolerance(2.0e-2);
            var ch4 = collection.FirstOrDefault<ISpeciesThermodynamicFunctions>("CH4");
            // wiki: standard state properties
            Assert.Equal(-74.87e+3, ch4.StandardMolarEnthalpyChangeOfFormation(), comparer);
            Assert.Equal(186.61, ch4.StandardMolarEntropy(), comparer);
            Assert.Equal(35.69, ch4.StandardHeatCapacityAtConstantPressure(), comparer);

            // http://webbook.nist.gov/cgi/cbook.cgi?ID=C74828&Mask=1
            // Gurvich, Veyts, et al., 1989
            Assert.Equal(46.63, ch4.MolarCp(500.0), comparer);
            Assert.Equal(69.14, ch4.MolarCp(900), comparer);
            Assert.Equal(77.92, ch4.MolarCp(1100), comparer);
            Assert.Equal(90.86, ch4.MolarCp(1500), comparer);
            Assert.Equal(99.51, ch4.MolarCp(1900), comparer);
            Assert.Equal(102.83, ch4.MolarCp(2100), comparer);
            Assert.Equal(108.23, ch4.MolarCp(2500), comparer);
            Assert.Equal(113.55, ch4.MolarCp(3000), comparer);
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
                    else if (tf.Name.Equals("auutherm.dat.txt", StringComparison.OrdinalIgnoreCase)
                        && ex.AllMessages.Count == 2
                        && ex.AllMessages[0].Contains("1774")
                        && ex.AllMessages[1].Contains("2594"))
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    else if (tf.Name.Equals("i-pentanol_therm_v33L-cl_release_dat.txt", StringComparison.OrdinalIgnoreCase)
                        && ex.AllMessages.Count == 2
                        && ex.AllMessages[0].Contains("2391")
                        && ex.AllMessages[1].Contains("2471"))
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
