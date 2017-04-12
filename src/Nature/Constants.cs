namespace Nature
{
    using System;

    public static partial class Constants
    {
        /// <summary>
        /// International Steam Table calorie (1956) (4.1868 [J])
        /// </summary>
        /// <remarks>
        /// The 5th International Conference on the Properties of Steam (London, July 1956) adopted a set of mechanical equivalents to thermal units, now identified by the abbreviation IT or IST (International [Steam] Tables), which made 1 Btu/lb exactly equal to 2326 J/kg.  One kilocalorie per kilogram is 1.8 times this ratio, because there are 1.8°F in 1°C (this factor applies to ideal units although the actual energy needed to raise a given mass of water, say, 10°C may be slightly different from 1.8 times what's needed to raise it 10°F).  An IT calorie is thus exactly 4.1868 J.  When the avoirdupois pound was finally defined in metric terms as exactly 0.45359237 kg  (effective January 1, 1959), the IT Btu became equal to exactly 1055.05585262 J (namely, 2326 J times the ratio of the pound to the kilogram).  The rarely used "centigrade heat unit" (chu) is 1.8 Btu.
        /// </remarks>
        public const double Calorie = Constants.InternationalSteamTableCalorie1956;

        #region Lennard-Jones potential Collision Integrals

        /// <summary>
        /// Lennard-Jones potential (k=1, l=1)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp11 = new double[]
        {
            6.96945701e-001, 3.39628861e-001,  1.32575555e-002, -3.41509659e-002,
            7.71359429e-003, 6.16106168e-004, -3.27101257e-004,  2.51567029e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=1, l=2)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp12 = new double[]
        {
            8.32155094e-001,    3.49407514e-001,    -3.82036372e-002,   -2.43853109e-002,
            1.77530715e-002,    -4.14950252e-003,   4.36550333e-004,    -1.73701462e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=1, l=3)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp13 = new double[]
        {
            9.29535342e-001,  3.26822830e-001, -6.07283688e-002, -7.56821423e-003,
            1.63615168e-002, -5.18498343e-003,  6.92526753e-004, -3.44119953e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=2, l=2)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp22 = new double[]
        {
            6.33225679e-001, 3.14473541e-001,  1.78229325e-002, -3.99489493e-002,
            8.98483088e-003, 7.00167217e-004, -3.82733808e-004,  2.97208112e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=2, l=3)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp23 = new double[]
        {
            7.23030826e-001,  3.29104000e-001, -2.67292995e-002, -3.44369327e-002,
            1.98152760e-002, -4.11285484e-003,  3.79226022e-004, -1.26060982e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=2, l=4)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp24 = new double[]
        {
            7.95169226e-001,  3.19782251e-001, -5.38414744e-002, -2.12220929e-002,
            2.15047718e-002, -6.07704776e-003,  7.61916484e-004, -3.61903000e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=2, l=5)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp25 = new double[]
        {
            8.52350666e-001,  3.00012768e-001, -6.55771424e-002, -7.46253415e-003,
            1.79687266e-002, -5.97639013e-003,  8.29762617e-004, -4.26186964e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=2, l=6)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp26 = new double[]
        {
            8.97691522e-001,  2.78653050e-001, -6.78654153e-002,  3.37356294e-003,
            1.30418817e-002, -5.00672341e-003,  7.41675713e-004, -3.96634865e-005
        };

        /// <summary>
        /// Lennard-Jones potential (k=4, l=4)  A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static readonly double[] _ljp44 = new double[]
        {
            7.28881625e-001,  3.26112535e-001, -3.23478489e-002, -3.18167670e-002,
            1.95359171e-002, -4.22445150e-003,  4.08438365e-004, -1.45325497e-005
        };

        /// <summary>
        /// Calculates Lennard-Jones potential using the A8 - polynomial approximation 
        /// </summary>
        ///<remarks>
        /// Evlampiev, A. (2007). Numerical combustion modeling for complex reaction systems. TUE p. 41
        ///</remarks>
        private static double CollisionIntegral(double[] coef, double tstar)
        {
            double Omega = 0.0;
            double factor = 1.0;
            double lnT = Math.Log(tstar);
            for (int i = 0; i <= 7; i++, factor *= lnT)
            {
                Omega += factor * coef[i];
            }
            return (1.0 / Omega);
        }

        /// <summary>
        /// Calculates the Lennard-Jones (k=1, l=1) potential for the given reduced temperature
        /// </summary>
        public static double LJP11(double tstar) { return CollisionIntegral(_ljp11, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=1, l=2) potential for the given reduced temperature
        /// </summary>
        public static double LJP12(double tstar) { return CollisionIntegral(_ljp12, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=1, l=3) potential for the given reduced temperature
        /// </summary>
        public static double LJP13(double tstar) { return CollisionIntegral(_ljp13, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=2, l=2) potential for the given reduced temperature
        /// </summary>
        public static double LJP22(double tstar) { return CollisionIntegral(_ljp22, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=2, l=3) potential for the given reduced temperature
        /// </summary>
        public static double LJP23(double tstar) { return CollisionIntegral(_ljp23, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=2, l=4) potential for the given reduced temperature
        /// </summary>
        public static double LJP24(double tstar) { return CollisionIntegral(_ljp24, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=2, l=5) potential for the given reduced temperature
        /// </summary>
        public static double LJP25(double tstar) { return CollisionIntegral(_ljp25, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=2, l=6) potential for the given reduced temperature
        /// </summary>
        public static double LJP26(double tstar) { return CollisionIntegral(_ljp26, tstar); }

        /// <summary>
        /// Calculates the Lennard-Jones (k=4, l=4) potential for the given reduced temperature
        /// </summary>
        public static double LJP44(double tstar) { return CollisionIntegral(_ljp44, tstar); }

        #endregion

    }
}
