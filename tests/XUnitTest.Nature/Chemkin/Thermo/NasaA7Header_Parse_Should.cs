namespace Nature.Chemkin.Thermo
{
    using Moq;
    using Nature.Common;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Text;
    using Xunit;

    public class NasaA7Header_Parse_Should
    {
        [Fact]
        public void SucceedWithValidHeaders()
        {
            var header = NasaA7Header.Parse(NasaA7HeaderResource.CH3CHO);
            Assert.Equal("CH3CHO", header.SpeciesCode);
            Assert.Equal("L 8/88", header.Tag);
            Assert.Equal(3, header.ElementCodes.Count());
            Assert.Equal(2.0, header["C"]);
            Assert.Equal(4.0, header["H"]);
            Assert.Equal(1.0, header["O"]);
            Assert.Equal(ThermodynamicPhase.Gas, header.Phase);
            Assert.Equal(200.0, header.LowTemperature);
            Assert.Equal(1000.0, header.CommonTemperature);
            Assert.Equal(6000.0, header.HighTemperature);

            header = NasaA7Header.Parse(NasaA7HeaderResource.BIN20);
            Assert.Equal("BIN20", header.SpeciesCode);
            Assert.Equal(2, header.ElementCodes.Count());
            Assert.Equal(12972032, header["C"]);
            Assert.Equal(1622016, header["H"]);
            Assert.Equal(ThermodynamicPhase.Gas, header.Phase);
            Assert.Equal(300.0, header.LowTemperature);
            Assert.Equal(1401.0, header.CommonTemperature);
            Assert.Equal(5000.0, header.HighTemperature);
        }


        [Fact]
        public void ThrowOnMissingSpeciesCode()
        {
            string header = Regex.Replace(NasaA7HeaderResource.CH3CHO, "^.{18}", m => new string((char)32, m.Length));
            string expectedStatement = Guid.NewGuid().ToString();
            
            var context = new DeserializationContext();
            var messageBuilder = new Mock<INasaA7DiagnosticsMessageBuilder>();
            messageBuilder.Setup(m => m.SpeciesCodeIsMissing()).Returns(expectedStatement);
            context.Push(messageBuilder.Object);

            var ex = Assert.Throws<ChemkinFormatException>(()=> NasaA7Header.Parse(header, context));
            Assert.True(ex.Message.Contains(expectedStatement));
            Assert.Equal(new TextPosition(1, 1), ex.Position);
        }

        [Fact]
        public void ThrowOnMissingChemicalFormula()
        {
            string header = Regex.Replace(NasaA7HeaderResource.CH3CHO, "(?<=^.{24}).{20}", m => new string((char)32, m.Length));
            string expectedStatement = Guid.NewGuid().ToString();

            var context = new DeserializationContext();
            var messageBuilder = new Mock<INasaA7DiagnosticsMessageBuilder>();
            messageBuilder.Setup(m => m.MissingChemicalFormula("CH3CHO")).Returns(expectedStatement);
            context.Push(messageBuilder.Object);

            var ex = Assert.Throws<ChemkinFormatException>(() => NasaA7Header.Parse(header, context));
            Assert.True(ex.Message.Contains(expectedStatement));
            Assert.Equal(new TextPosition(1, 25), ex.Position);
        }

        [Fact]
        public void ThrowOnInvalidPhaseIdentifier()
        {
            string header = Regex.Replace(NasaA7HeaderResource.CH3CHO, "(?<=^.{44}).", "Z");
            string expectedStatement = Guid.NewGuid().ToString();

            var context = new DeserializationContext();
            var messageBuilder = new Mock<INasaA7DiagnosticsMessageBuilder>();
            messageBuilder.Setup(m => m.InvalidPhaseIdentifier("CH3CHO", "Z")).Returns(expectedStatement);
            context.Push(messageBuilder.Object);

            var ex = Assert.Throws<ChemkinFormatException>(() => NasaA7Header.Parse(header, context));
            Assert.True(ex.Message.Contains(expectedStatement));
            Assert.Equal(new TextPosition(1, 45), ex.Position);
        }
    }
}
