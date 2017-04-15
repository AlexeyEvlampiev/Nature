namespace Nature.Common
{
    using System;
    using System.Diagnostics;

    public struct NasaA7ApproximationRange
    {
        [DebuggerNonUserCode]
        public NasaA7ApproximationRange(double[] approximation) : this()
        {
            A1 = approximation[0];
            A2 = approximation[1];
            A3 = approximation[2];
            A4 = approximation[3];
            A5 = approximation[4];
            A6 = approximation[5];
            A7 = approximation[6];            
        }

        [DebuggerNonUserCode]
        public NasaA7ApproximationRange(double a1, double a2, double a3, double a4, double a5, double a6, double a7) : this()
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
            A5 = a5;
            A6 = a6;
            A7 = a7;
        }

        public NasaA7ApproximationRange RebaseA1(double reducedCp, double temperature)
        {
            double computedA1 = reducedCp - CalcReducedCp(temperature) + A1;
            var rebased = new NasaA7ApproximationRange(computedA1, A2, A3, A4, A5, A6, A7);
            Debug.Assert(DoubleComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 1.0e-8)
                .Equals(reducedCp, rebased.CalcReducedCp(temperature)));
            return rebased;
        }

        public NasaA7ApproximationRange RebaseA6(double reducedH, double temperature)
        {
            double computedA6 = temperature * (reducedH - CalcReducedH(temperature)) + A6;
            var rebased = new NasaA7ApproximationRange(A1, A2, A3, A4, A5, computedA6, A7);
            Debug.Assert(DoubleComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 1.0e-8)
                .Equals(reducedH, rebased.CalcReducedH(temperature)));
            return rebased;
        }

        public NasaA7ApproximationRange RebaseA7(double reducedS, double temperature)
        {
            double computedA7 = reducedS - CalcReducedS(temperature) + A7;
            var rebased = new NasaA7ApproximationRange(A1, A2, A3, A4, A5, A6, computedA7);
            Debug.Assert(DoubleComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 1.0e-8)
                .Equals( reducedS, rebased.CalcReducedS(temperature)));
            return rebased;
        }

        public NasaA7ApproximationRange Rebase(double reducedCp, double reducedH, double reducedS, double temperature)
        {
            return 
                RebaseA1(reducedCp, temperature)
                .RebaseA6(reducedH, temperature)
                .RebaseA7(reducedS, temperature);
        }

        public readonly double A1;
        public readonly double A2;
        public readonly double A3;
        public readonly double A4;
        public readonly double A5;
        public readonly double A6;
        public readonly double A7;

        [DebuggerNonUserCode]
        public double CalcReducedCp(double temperature)
        {
            double t2 = temperature;
            double t3 = t2 * temperature;
            double t4 = t3 * temperature;
            double t5 = t4 * temperature;            
            return A1 + A2 * t2 + A3 * t3 + A4 * t4 + A5 * t5;
        }

        [DebuggerNonUserCode]
        public double CalcReducedH(double temperature)
        {
            double t2 = temperature;
            double t3 = t2 * temperature;
            double t4 = t3 * temperature;
            double t5 = t4 * temperature;
            return A1 + A2 / 2.0 * t2 + A3 / 3.0 * t3 + A4 / 4.0 * t4 + A5 / 5.0 * t5 + A6 / temperature;
        }

        [DebuggerNonUserCode]
        public double CalcReducedS(double temperature)
        {
            double t2 = temperature;
            double t3 = t2 * temperature;
            double t4 = t3 * temperature;
            double t5 = t4 * temperature;
            return A1 * Math.Log(temperature) + A2  * t2 + A3 / 2.0 * t3 + A4 / 3.0 * t4 + A5 / 4.0 * t5 + A7;
        }

        public bool Equals(NasaA7ApproximationRange other, DoubleComparer comparer)
        {
            return
                comparer.Equals(A1, other.A1)
                && comparer.Equals(A2, other.A2)
                && comparer.Equals(A3, other.A3)
                && comparer.Equals(A4, other.A4)
                && comparer.Equals(A5, other.A5)
                && comparer.Equals(A6, other.A6)
                && comparer.Equals(A7, other.A7);
        }
    }
}
