
namespace Nature
{
    using System;
    using System.Diagnostics;

    public sealed partial class IdealGasState
    {
        readonly IdealGasModel _model;    
        

        public IdealGasState(IdealGasModel model)
        {
            if (ReferenceEquals(model, null))
                throw new ArgumentNullException(nameof(model));
            _model = model;            
            _speciesMoleFractions = new double[model.NumberOfSpecies];
            _speciesMoleFractions[0] = 1.0;
            SetTPX(Constants.StandardStateTemperature, Constants.Atmosphere, _speciesMoleFractions);
        }

        public void SetTPX(double temperature, double pressure, double[] speciesMoleFractions)
        {
            this.DropState();
            this.Temperature = temperature;
            this.Pressure = pressure;
            this.SpeciesMoleFractions = speciesMoleFractions;
        }

        public void SetTPY(double temperature, double pressure, double[] speciesMassFractions)
        {
            this.DropState();
            this.Temperature = temperature;
            this.Pressure = pressure;
            this.SpeciesMassFractions = speciesMassFractions;
        }

        private double CalculateTemperature()
        {
            throw new NotImplementedException();
        }

        private double CalculatePressure()
        {
            throw new NotImplementedException();
        }


        private double CalculateMolarMass()
        {
            throw new NotImplementedException();
        }

        private void CalculateSpeciesMassFractions(double[] speciesMassFractions)
        {
            throw new NotImplementedException();
        }


        private void CalculateSpeciesMoleFractions(double[] x)
        {
            if (_hasSpeciesMassFractions)
            {
                var y = _speciesMassFractions;
                var w = _model.SpeciesMolarMasses;                
                double molesPer1kg = 0.0;
                for (int i = 0; i < y.Length; ++i)
                {
                    double moles = y[i] / w[i];
                    x[i] = moles;
                    molesPer1kg += moles;
                }

                MolarMass = 1.0 / molesPer1kg;
                decimal decimalXsum = 0;
                for (int i = 0; i < y.Length; ++i)
                {
                    x[i] /= molesPer1kg;
                    decimalXsum += (decimal)x[i];
                }

                double xsum = (double)decimalXsum;
                for (int i = 0; i < y.Length; ++i)
                {
                    x[i] /= xsum;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void NormalizeFractions(double[] fractions)
        {
            decimal decimalSum = 0;
            for (int i = 0; i < fractions.Length; ++i)
            {
                double fr = fractions[i];                
                if (fr > 0.0)
                    decimalSum += (decimal)fr;
                else
                    fractions[i] = 0.0;
            }

            double sum = (double)decimalSum;
            for (int i = 0; i < fractions.Length; ++i)
            {
                fractions[i] /= sum;
            }
        }

        [DebuggerStepThrough]
        partial void OnSpeciesMoleFractionsChanged()
        {
            NormalizeFractions(_speciesMoleFractions);
        }

        [DebuggerStepThrough]
        partial void OnSpeciesMassFractionsChanged()
        {
            NormalizeFractions(_speciesMassFractions);
        }
    }
}
