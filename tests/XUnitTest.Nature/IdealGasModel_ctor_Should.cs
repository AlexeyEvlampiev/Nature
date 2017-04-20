namespace Nature
{
    using Nature.Chemkin.Thermo;
    using Xunit;

    public class IdealGasModel_ctor_Should
    {
        [Fact]
        public void Work()
        {
            var thermo = ThermoCollection.Parse(ThermoCollectionsResource.thermo30);
            var model = new IdealGasModel(thermo);
            //var x = model.SpeciesMolarMasses.Equals(model.SpeciesMolarMasses, DoubleComparer.FromAbsoluteTolerance(1));
        }
    }
}
