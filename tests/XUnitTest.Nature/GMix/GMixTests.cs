namespace Nature.GMix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GMixTests
    {
        readonly IdealGasModel _model;
        

        public GMixTests()
        {
            _model = Chemkin.GasPhaseMechanism.CreateAsync(
                Chemkin.Thermo.ThermoCollectionsResource.thermo30,
                CancellationToken.None)
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public async Task Test()
        {            
            string baseScript = @"
                // example 1
                C3H8 = 0.1 /*
                    some comment...
                */
                O2 = ?
            ";
            var testCases = new Dictionary<string, string>();
            testCases.Add(baseScript, "anonymous");
            testCases.Add($@"{{
                {baseScript}
            }}", "anonymous");
            testCases.Add($@"""my-mixture"": {{
                {baseScript}
            }}", "my-mixture");


            foreach (var pair in testCases)
            {
                //string script = pair.Key;
                //string expectedMixtureName = pair.Value;
                //var output = await GMixMarkup.ParseAsync(script, _model);
                //var collections = output.Single();
                //var mixture = collections.Single();
                //var state = mixture.IdealGasState;

                //var comparer = DoubleComparer.FromRelativeTolerance(1.0e-15);
                //Assert.Equal(expectedMixtureName, mixture.Name, StringComparer.Ordinal);
                //Assert.Equal(Constants.StandardStateTemperature, state.Temperature);
                //Assert.Equal(Constants.Atmosphere, state.Pressure);
                //Assert.Equal(0.1, state.SpeciesMoleFraction("C3H8"), comparer);
                //Assert.Equal(0.9, state.SpeciesMoleFraction("O2"), comparer);
            }            
        }        
    }
}
