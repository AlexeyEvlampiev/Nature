namespace Nature.Common
{
    using System;

    public struct NasaA7ApproximationRange
    {
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

        public readonly double A1;
        public readonly double A2;
        public readonly double A3;
        public readonly double A4;
        public readonly double A5;
        public readonly double A6;
        public readonly double A7;

        public double CalcReducedCp(double temperature)
        {
            double t2 = temperature;
            double t3 = t2 * temperature;
            double t4 = t3 * temperature;
            double t5 = t4 * temperature;            
            return A1 + A2 * t2 + A3 * t3 + A4 * t4 + A5 * t5;
        }

        public double CalcReducedH(double temperature)
        {
            double t2 = temperature;
            double t3 = t2 * temperature;
            double t4 = t3 * temperature;
            double t5 = t4 * temperature;
            return A1 + A2 / 2.0 * t2 + A3 / 3.0 * t3 + A4 / 4.0 * t4 + A5 / 5.0 * t5 + A6 / temperature;
        }

        public double CalcReducedS(double temperature)
        {
            double t2 = temperature;
            double t3 = t2 * temperature;
            double t4 = t3 * temperature;
            double t5 = t4 * temperature;
            return A1 * Math.Log(temperature) + A2  * t2 + A3 / 2.0 * t3 + A4 / 3.0 * t4 + A5 / 4.0 * t5 + A7;
        }

    }
}
