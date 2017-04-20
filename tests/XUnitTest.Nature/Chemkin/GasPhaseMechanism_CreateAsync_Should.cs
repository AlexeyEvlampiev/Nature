namespace Nature.Chemkin
{
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GasPhaseMechanism_CreateAsync_Should
    {
        [Fact]
        public async Task WorkAsync()
        {
            string[] resources = new string[] { Thermo.ThermoCollectionsResource.thermo30 };
            var mechanism = await GasPhaseMechanism.CreateAsync(resources, CancellationToken.None);
            var state = new IdealGasState(mechanism);

        }
    }
}
