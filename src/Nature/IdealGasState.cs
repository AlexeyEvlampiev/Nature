
namespace Nature
{
    using System;
    using System.Diagnostics;

    public sealed partial class IdealGasState
    {
        readonly IdealGasModel _model;

        public IdealGasModel Model => _model;

        public IdealGasState(IdealGasModel model)
        {
            if (ReferenceEquals(model, null))
                throw new ArgumentNullException(nameof(model));
            _model = model;            
            _speciesMoleFractions = _model.AllocateSpeciesArray();
            _speciesMoleFractions[0] = 1.0;
            SetTPX(Constants.StandardStateTemperature, Constants.Atmosphere, _speciesMoleFractions);
        }

        public IdealGasState(IdealGasModel model, ReadOnlyIdealGasState other)
        {
            _model = model;
            Set(other);
        }

        public IdealGasState(IdealGasModel model, string mixtureDefinition)
        {
            mixtureDefinition = mixtureDefinition.Trim();
            if (model.IsSpeciesCode(mixtureDefinition))
            {
                string speciesCode = mixtureDefinition;
                _speciesMoleFractions = model.AllocateSpeciesArray();
                model.SetSpecies(speciesCode, _speciesMoleFractions, 1.0);
                SetTPX(Constants.StandardStateTemperature, Constants.Atmosphere, _speciesMoleFractions);
            }
            else
            {
                //var gmixOutput = GMixMarkup.ParseAsync(mixtureDefinition, model).GetAwaiter().GetResult();
                //var collection = gmixOutput.Single();
                //var mixture = collection.Single();
                //this.Set(mixture.IdealGasState);
            }
        }

        [DebuggerNonUserCode]
        public double SpeciesMoleFraction(string speciesCode) => _model.GetSpecies(speciesCode, SpeciesMoleFractions);

        public void Set(ReadOnlyIdealGasState other)
        {
            if (ReferenceEquals(Model, other.Model))
            {
                SetTPX(other.Temperature, other.Pressure, other.SpeciesMoleFractions);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void SetTPX(double temperature, double pressure, ReadOnlyArray<double> speciesMoleFractions)
        {
            this.DropState();
            this.Temperature = temperature;
            this.Pressure = pressure;
            this.SpeciesMoleFractions = speciesMoleFractions;
        }

        public void SetTPY(double temperature, double pressure, ReadOnlyArray<double> speciesMassFractions)
        {
            this.DropState();
            this.Temperature = temperature;
            this.Pressure = pressure;
            this.SpeciesMassFractions = speciesMassFractions;
        }

        private double CalculateMolarDensity()
        {
            if (_hasTemperature)
            {
                if (_hasPressure)
                {
                    return _pressure / _temperature / Constants.Rgas;
                }
            }

            if (_hasMassDensity)
            {
                return _massDensity / MolarMass;
            }

            throw new InvalidOperationException();
        }


        private double CalculateMassDensity()
        {
            return MolarDensity * MolarMass;
        }


        private double CalculateTemperature()
        {
            throw new NotImplementedException();
        }

        private double CalculatePressure()
        {
            throw new NotImplementedException();
        }

        [DebuggerStepThrough]
        private void CalculateSpeciesReducedCp(double[] cp) => _model.CalculateSpeciesReducedCp(this.Temperature, cp);

        [DebuggerStepThrough]
        private void CalculateSpeciesReducedH(double[] h) => _model.CalculateSpeciesReducedH(this.Temperature, h);

        [DebuggerStepThrough]
        private void CalculateSpeciesReducedS(double[] s) => _model.CalculateSpeciesReducedS(this.Temperature, s);

        private void CalculateSpeciesLogX(double[] logX)
        {
            var x = SpeciesMoleFractions;
            for (int i = 0; i < logX.Length; ++i)
            {
                logX[i] = Math.Log(double.Epsilon + x[i]);
            }
        }


        private double CalculateSpeedOfSound()
        {
            double cp = ReducedCp;
            double gamma = cp / (cp - 1.0d);
            return Math.Sqrt(gamma * Constants.Rgas * this.Temperature / this.MolarMass);
        }

        private double CalculateMolarMass()
        {
            if (!_hasSpeciesMoleFractions)
            {
                CalculateSpeciesMoleFractions();
            }
            else if (!_hasSpeciesMassFractions)
            {
                CalculateSpeciesMassFractions();
            }

            if (!_hasMolarMass)
                throw new InvalidOperationException();
           
            return _molarMass;
        }

        private double CalculateReducedCp()
        {
            var x = SpeciesMoleFractions;
            var cp = SpeciesReducedCp;
            double reducedCp = 0.0;
            for (int i = 0; i < x.Length; ++i)
            {
                reducedCp += x[i] * cp[i];
            }
            return reducedCp;
        }


        private double CalculateReducedH()
        {
            var x = SpeciesMoleFractions;
            var h = SpeciesReducedH;
            double reducedH = 0.0;
            for (int i = 0; i < x.Length; ++i)
            {
                reducedH += x[i] * h[i];
            }
            return reducedH;
        }

        private double CalculateReducedS()
        {
            var x = SpeciesMoleFractions;
            var s = SpeciesReducedS;
            var logX = SpeciesLogX;
            double reducedS = 0.0;
            for (int i = 0; i < x.Length; ++i)
            {
                reducedS += x[i] * (s[i] - logX[i]);
            }
            return reducedS;
        }

        private void CalculateSpeciesMassFractions(double[] y)
        {
            if (_hasSpeciesMoleFractions)
            {
                var x = _speciesMoleFractions;
                var w = _model.SpeciesMolarMasses;
                double molarMass = 0.0;
                for (int i = 0; i < x.Length; ++i)
                {
                    molarMass += y[i] = x[i] * w[i];
                }

                if (!_hasMolarMass)
                {
                    MolarMass = molarMass;
                }

                for(int i = 0; i < x.Length; ++i)
                {
                    y[i] /= molarMass;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
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

                if (!_hasMolarMass)
                {
                    MolarMass = 1.0 / molesPer1kg;
                }

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
