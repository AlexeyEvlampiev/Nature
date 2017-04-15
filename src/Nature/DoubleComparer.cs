namespace Nature
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public struct DoubleComparer : IEqualityComparer<double>, IComparer<double>
    {
        public readonly double AbsoluteTolerance;

        public readonly double RelativeTolerance;

        [DebuggerNonUserCode]
        private DoubleComparer(double absoluteTolerance, double relativeTolerance) : this()
        {
            AbsoluteTolerance = absoluteTolerance < 0.0d ? (-absoluteTolerance) : absoluteTolerance;
            RelativeTolerance = relativeTolerance < 0.0d ? (-relativeTolerance) : relativeTolerance;
        }

        [DebuggerNonUserCode]
        public static DoubleComparer FromAbsoluteAndRelativeTolerances(double absoluteTolerance, double relativeTolerance) => new DoubleComparer(absoluteTolerance, relativeTolerance);

        [DebuggerNonUserCode]
        public static DoubleComparer FromAbsoluteTolerance(double absoluteTolerance) => new DoubleComparer(absoluteTolerance, 0.0d);

        [DebuggerNonUserCode]
        public static DoubleComparer FromRelativeTolerance(double relativeTolerance) => new DoubleComparer(0.0d, relativeTolerance);

        public int Compare(double x, double y) => this.Equals(x, y) ? 0 : (x < y) ? (-1) : 0;

        public int? Compare(double? x, double? y)
        {
            return (x.HasValue && y.HasValue) ? (int?)this.Compare(x.Value, y.Value) : null;
        }
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

        public bool Equals(double? x, double? y)
        {
            if (x.HasValue && y.HasValue)
                return this.Equals(x.Value, y.Value);
            return false;
        }

       

        [DebuggerNonUserCode]
        public int GetHashCode(double obj) => obj.GetHashCode();
    }
}
