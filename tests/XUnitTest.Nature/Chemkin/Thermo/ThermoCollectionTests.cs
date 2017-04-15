namespace Nature.Chemkin.Thermo
{
    using Nature.Common;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
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

            var comparer = DoubleComparer.FromRelativeTolerance(2.0e-2);
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
        public void Super()
        {
            string baseDir = System.AppContext.BaseDirectory;
            var dirName = Path.Combine(baseDir, "Chemkin", "Mechanisms");
            var mechanisms = new DirectoryInfo(dirName);
            if (!mechanisms.Exists)
                throw new DirectoryNotFoundException(mechanisms.FullName);
            var thermoFiles = mechanisms.GetFiles("*therm*", SearchOption.AllDirectories);

            var sb = new StringBuilder();
            foreach (var tf in thermoFiles)
            {
                try
                {
                    string text = File.ReadAllText(tf.FullName);
                    RunTests(text);
                }
                catch (ChemkinException ex)
                {
                    Debug.WriteLine(ex.Message);
                    sb.AppendLine(ex.Message);
                }
            }

            //throw new Exception(sb.ToString());
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
