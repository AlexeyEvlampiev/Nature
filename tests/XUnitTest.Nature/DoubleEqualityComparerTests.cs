namespace Nature
{
    using Xunit;

    public class DoubleEqualityComparerTests
    {
        [Fact]
        public void Ctor()
        {
            var target = new DoubleEqualityComparer();
            Assert.Equal(target.AbsoluteTolerance, 0.0);
            Assert.Equal(target.RelativeTolerance, 0.0);

            target = DoubleEqualityComparer.FromAbsoluteTolerance(1.0);
            Assert.Equal(target.AbsoluteTolerance, 1.0);
            Assert.Equal(target.RelativeTolerance, 0.0);

            target = DoubleEqualityComparer.FromRelativeTolerance(1.0e-2);
            Assert.Equal(target.AbsoluteTolerance, 0.0);
            Assert.Equal(target.RelativeTolerance, 1.0e-2);

            target = DoubleEqualityComparer.FromAbsoluteAndRelativeTolerances(1.0, 1.0e-2);
            Assert.Equal(target.AbsoluteTolerance, 1.0);
            Assert.Equal(target.RelativeTolerance, 1.0e-2);

            target = DoubleEqualityComparer.FromAbsoluteAndRelativeTolerances(-1.0, -2.0);
            Assert.Equal(target.AbsoluteTolerance, 1.0);
            Assert.Equal(target.RelativeTolerance, 2.0);
        }

        [Fact]
        public void Equal()
        {
            var target = new DoubleEqualityComparer();
            Assert.Equal(1.0, 1.0, target);
            Assert.NotEqual(1.0, 1.0 + 1.0e-10, target);


            target = DoubleEqualityComparer.FromAbsoluteTolerance(1.0);
            Assert.Equal(1.0, 2.0, target);
            Assert.Equal(0.0, 1.0, target);
            Assert.Equal(-1.0, 0.0, target);
            Assert.NotEqual(1.0, 3.0, target);
            Assert.NotEqual(-1.0, 1.0, target);

            target = DoubleEqualityComparer.FromRelativeTolerance(1.0e-2);
            Assert.Equal(1.0, 1.01, target);
            Assert.NotEqual(1.0, 1.02, target);
            Assert.Equal(-1.0, -1.01, target);
            Assert.NotEqual(-1.0, -1.02, target);

            //target = DoubleEqualityComparer.FromAbsoluteAndRelativeTolerances(1.0, 1.0e-2);
            //Assert.Equal(target.AbsoluteTolerance, 1.0);
            //Assert.Equal(target.RelativeTolerance, 1.0e-2);
        }
    }
}
