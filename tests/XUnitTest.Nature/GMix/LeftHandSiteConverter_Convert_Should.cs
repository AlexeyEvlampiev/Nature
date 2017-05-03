namespace Nature.GMix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Xunit;

    public class LeftHandSiteConverter_Convert_Should
    {
        [Fact]
        public void Work()
        {
            var converter = new FakeLeftHandSiteConverter(true);

            Assert.True(converter.Convert("$t") is TemperatureLeftHandSide);
            Assert.True(converter.Convert("$temp") is TemperatureLeftHandSide);
            Assert.True(converter.Convert("$temperature") is TemperatureLeftHandSide);

            Assert.True(converter.Convert("$(t)") is TemperatureLeftHandSide);
            Assert.True(converter.Convert("$(temp)") is TemperatureLeftHandSide);
            Assert.True(converter.Convert("$(temperature)") is TemperatureLeftHandSide);


            Assert.True(converter.Convert("$p") is PressureLeftHandSide);
            Assert.True(converter.Convert("$pres") is PressureLeftHandSide);
            Assert.True(converter.Convert("$pressure") is PressureLeftHandSide);

            Assert.True(converter.Convert("$(p)") is PressureLeftHandSide);
            Assert.True(converter.Convert("$(pres)") is PressureLeftHandSide);
            Assert.True(converter.Convert("$(pressure)") is PressureLeftHandSide);

        }

        
    }
}
