
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

            double[] x = model.AllocateSpeciesArray<double>();
            state.SpeciesMoleFractions.CopyTo(ref x);
            state.SetTPX(Constants.StandardStateTemperature, Constants.Atmosphere, x);
            double actualMolarMass = state.MolarMass;

            var comparer = DoubleComparer.FromRelativeTolerance(1.0e-15);
            Assert.Equal(expectedMolarMass, actualMolarMass, comparer);        
            Assert.True(comparer.Equals(y, state.SpeciesMassFractions));

        }
    }
}
