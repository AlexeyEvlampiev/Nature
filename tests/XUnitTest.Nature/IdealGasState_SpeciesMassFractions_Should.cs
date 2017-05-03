
namespace Nature
{
    using Nature.Chemkin;
    using Nature.Chemkin.Thermo;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class IdealGasState_SpeciesMassFractions_Should
    {
        [Fact]
        public async Task Work()
        {
            var model = await GasPhaseMechanism.CreateAsync(ThermoCollectionsResource.thermo30, CancellationToken.None);
            double[] y = model.AllocateSpeciesArray<double>();
            model.SetSpecies("H", y, 0.5);
            model.SetSpecies("O2", y, 0.5);

            var state = new IdealGasState(model);
            state.SetTPY(Constants.StandardStateTemperature, Constants.Atmosphere, y);
            double expectedMolarMass = state.MolarMass;

            double[] x = state.SpeciesMoleFractions.ToArray();
            state.SetTPX(Constants.StandardStateTemperature, Constants.Atmosphere, x);
            double actualMolarMass = state.MolarMass;

            var comparer = DoubleComparer.FromRelativeTolerance(1.0e-15);
            Assert.Equal(expectedMolarMass, actualMolarMass, comparer);        
            Assert.True(comparer.Equals(y, state.SpeciesMassFractions));

            x = model.AllocateSpeciesArray<double>();
            model.SetSpecies("N2", x, 1.0);
            state.SetTPX(Constants.StandardStateTemperature, Constants.Atmosphere, x);
            var cp = state.ReducedCp;            
            var c = state.SpeedOfSound;


            for (double t = Constants.StandardStateTemperature; t < 1000; t *= 1.1)
            {
                state.SetTPX(t, Constants.Atmosphere, x);
                double z = state.SpeedOfSound;
            }
        }
    }
}
