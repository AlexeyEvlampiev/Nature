using System;
using System.Collections.Generic;
using System.Text;

namespace Nature.Common
{
    public static class SpeciesThermoExtensions
    {
        public static double H(this ISpeciesThermo self, double temperature)
        {
            return temperature * Constants.Rgas * self.ReducedH(temperature);
        }
    }
}
