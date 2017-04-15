namespace Nature.Common
{
    using System;
    using System.Diagnostics;

    public class DefaultSpeciesThermodynamicFunctionsValidator : ISpeciesThermodynamicFunctionsValidator
    {
        public void Validate(
            ISpeciesThermodynamicFunctions target, 
            Action<string> onError, 
            Action<string> onWarning = null)
        {
            if (ReferenceEquals(target, null))
                throw new ArgumentNullException(nameof(target));
            if (ReferenceEquals(onError, null))
                throw new ArgumentNullException(nameof(onError));
            onWarning = onWarning ?? delegate { };

            double tmin = target.MinTemperature * 0.9;
            double tmax = target.MaxTemperature * 1.01;
            const double factor = 1.1;
            double currCp, currH, currS;
            double 
                prevTemperature = tmin,
                prevCp = target.ReducedCp(tmin), 
                prevH = target.ReducedH(tmin) * tmin, 
                prevS = target.ReducedS(tmin);
            var cmp = DoubleComparer.FromAbsoluteAndRelativeTolerances(1.0e-8, 3.0e-2);
            int countLowCpErrors = 0;
            int countDecreasingCpErrors = 0;
            int countDecreasingHErrors = 0;
            int countDecreasingSErrors = 0;
            int countDeltaHMissmatchErrors = 0;
            int countDeltaSMissmatchErrors = 0;
            for (double currTemperature = tmin * factor; 
                currTemperature <= tmax; 
                currTemperature *= factor)
            {
                currCp = target.ReducedCp(currTemperature);
                currH = target.ReducedH(currTemperature) * currTemperature;
                currS = target.ReducedS(currTemperature);
                if (countLowCpErrors == 0 && cmp.Compare(currCp, 2.5) < 0)
                {
                    countLowCpErrors++;
                    string message = CreateLowCpErrorMessage(currCp, currTemperature);
                    onError(message);
                }
                if (countDecreasingCpErrors == 0 && cmp.Compare(prevCp, currCp) > 0)
                {
                    countDecreasingCpErrors++;
                    string message = CreateDecreasingCpErrorMessage(currTemperature);
                    onError(message);
                }
                if (countDecreasingHErrors == 0 && cmp.Compare(prevH, currH) > 0)
                {
                    countDecreasingHErrors++;
                    string message = CreateDecreasingHErrorMessage(currTemperature);
                    onError(message);
                }
                if (countDecreasingSErrors == 0 && cmp.Compare(prevS, currS) > 0)
                {
                    countDecreasingSErrors++;
                    string message = CreateDecreasingSErrorMessage(currTemperature);
                    onError(message);
                }
                
                double estimatedDH = 0.5 * (currCp + prevCp) * (currTemperature - prevTemperature);
                double estimatedDS = 0.5 * (currCp + prevCp) * Math.Log(currTemperature / prevTemperature);
                double actualDH = currH - prevH;
                double actualDS = currS - prevS;
                if (countDeltaHMissmatchErrors == 0 &&!cmp.Equals(estimatedDH, actualDH))
                {
                    countDeltaHMissmatchErrors++;
                    string message = CreateDeltaHMissmatchErrorMessage(currTemperature);
                    onError(message);
                }

                if (countDeltaSMissmatchErrors == 0 && !cmp.Equals(estimatedDS, actualDS))
                {
                    countDeltaSMissmatchErrors++;
                    string message = CreateDeltaSMissmatchErrorMessage(currTemperature);
                    onError(message);
                }
                prevTemperature = currTemperature;
                prevCp = currCp;
                prevH = currH;
                prevS = currS;
            }
        }

        private string CreateDeltaSMissmatchErrorMessage(double t)
        {
            return $"Insufficient accuracy of the entropy (S) value computed for T = {t}K";
        }

        private string CreateDeltaHMissmatchErrorMessage(double t)
        {
            return $"Insufficient accuracy of the enthalpy (H) value computed for T = {t}K";
        }

        protected virtual string CreateLowCpErrorMessage(double cp, double t)
        {
            return $"Reduced Cp value of {cp} avaluated for the temperature of {t}K is below its possible minimum of 2.5";
        }

        protected virtual string CreateDecreasingCpErrorMessage(double t)
        {
            return $"The isobaric heat capacity (Cp) temperature dependency shows a slope at T = {t}K";
        }

        protected virtual string CreateDecreasingHErrorMessage(double t)
        {
            return $"The enthalpy (H) temperature dependency shows a slope at T = {t}K";
        }

        protected virtual string CreateDecreasingSErrorMessage(double t)
        {
            return $"The entropy (S) temperature dependency shows a slope at the temperature of {t}K";
        }
    }
}
