using Xunit;

namespace Nature.Common
{
    public class NasaA7ApproximationRangeTests
    {
        [Fact]
        public void Rebase()
        {
            double temperature = 1.0;
            double cp = 1.0, h = 1.0, s = 1.0;
            var range = new NasaA7ApproximationRange(1, 2, 3, 4, 5, 6, 7);
            var comparer = DoubleEqualityComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 1.0e-8);
            range = range.Rebase(cp, h, s, temperature);
            Assert.Equal(cp, range.CalcReducedCp(temperature), comparer);
            Assert.Equal(h, range.CalcReducedH(temperature), comparer);
            Assert.Equal(s, range.CalcReducedS(temperature), comparer);
        }
    }
}
