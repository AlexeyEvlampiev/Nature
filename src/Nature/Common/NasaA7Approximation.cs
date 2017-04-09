namespace Nature.Chemkin.Common
{
    public struct NasaA7Approximation
    {
        public NasaA7Approximation(double[] approximation) : this()
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

    }
}
