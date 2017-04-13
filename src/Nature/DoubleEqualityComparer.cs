namespace Nature
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public struct DoubleEqualityComparer : IEqualityComparer<double>
    {
        public readonly double AbsoluteTolerance;

        public readonly double RelativeTolerance;

        [DebuggerNonUserCode]
        private DoubleEqualityComparer(double absoluteTolerance, double relativeTolerance) : this()
        {
            AbsoluteTolerance = absoluteTolerance < 0.0d ? (-absoluteTolerance) : absoluteTolerance;
            RelativeTolerance = relativeTolerance < 0.0d ? (-relativeTolerance) : relativeTolerance;
        }

        [DebuggerNonUserCode]
        public static DoubleEqualityComparer FromAbsoluteAndRelativeTolerances(double absoluteTolerance, double relativeTolerance) => new DoubleEqualityComparer(absoluteTolerance, relativeTolerance);

        [DebuggerNonUserCode]
        public static DoubleEqualityComparer FromAbsoluteTolerance(double absoluteTolerance) => new DoubleEqualityComparer(absoluteTolerance, 0.0d);

        [DebuggerNonUserCode]
        public static DoubleEqualityComparer FromRelativeTolerance(double relativeTolerance) => new DoubleEqualityComparer(0.0d, relativeTolerance);

        public bool Equals(double x, double y)
        {            
            double absoluteDelta = (x - y);

            absoluteDelta = (absoluteDelta < 0.0d ? (-absoluteDelta) : absoluteDelta);
            if (absoluteDelta <= AbsoluteTolerance)
                return true;

            x = (x < 0.0d ? -x : x) + double.Epsilon;
            y = (y < 0.0d ? -y : y) + double.Epsilon;
            return 
                (absoluteDelta / x) < RelativeTolerance ||
                (absoluteDelta / y) < RelativeTolerance;
        }

        [DebuggerNonUserCode]
        public int GetHashCode(double obj) => obj.GetHashCode();
    }
}
