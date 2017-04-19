

namespace Nature
{
	using System.Diagnostics;

	public partial class IdealGasState
	{
		#region Private Fields 
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _temperature;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _pressure;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _molarMass;  
		 
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesMassFractions;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesMoleFractions;  

		 
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasTemperature;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasPressure;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasMolarMass;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesMassFractions;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesMoleFractions;  
		#endregion		

		#region Partial Methods 
		partial void OnTemperatureChanged();  
		partial void OnPressureChanged();  
		partial void OnMolarMassChanged();  
		partial void OnSpeciesMassFractionsChanged();  
		partial void OnSpeciesMoleFractionsChanged();  
		#endregion	

		private void DropState()
		{ 
			_hasTemperature = false;  
			_hasPressure = false;  
			_hasMolarMass = false;  
			_hasSpeciesMassFractions = false;  
			_hasSpeciesMoleFractions = false;   
		}


		 
		public double Temperature
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasTemperature)
					return _temperature;
				_temperature = CalculateTemperature();
				_hasTemperature = true;
				return _temperature;
			}
			[DebuggerStepThrough]
			private set
			{
				_temperature = value;
				_hasTemperature = true;
				OnTemperatureChanged(); 
			}
		}
		 
		public double Pressure
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasPressure)
					return _pressure;
				_pressure = CalculatePressure();
				_hasPressure = true;
				return _pressure;
			}
			[DebuggerStepThrough]
			private set
			{
				_pressure = value;
				_hasPressure = true;
				OnPressureChanged(); 
			}
		}
		 
		public double MolarMass
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasMolarMass)
					return _molarMass;
				_molarMass = CalculateMolarMass();
				_hasMolarMass = true;
				return _molarMass;
			}
			[DebuggerStepThrough]
			private set
			{
				_molarMass = value;
				_hasMolarMass = true;
				OnMolarMassChanged(); 
			}
		}
		 

				
		public ReadOnlyArray<double> SpeciesMassFractions
		{
			[DebuggerStepThrough]
			get
			{
				if(_speciesMassFractions == null)
				{
					_speciesMassFractions = new double[_model.NumberOfSpecies];
					_hasSpeciesMassFractions = false;
				}

				if(!_hasSpeciesMassFractions)
				{
					this.CalculateSpeciesMassFractions(_speciesMassFractions);
					_hasSpeciesMassFractions = true;
				}

				return _speciesMassFractions;
			}
			[DebuggerStepThrough]
			private set
			{
				if(_speciesMassFractions == null)
				{
					_speciesMassFractions = new double[_model.NumberOfSpecies];					
				}
				else if(_speciesMassFractions == value.Data)
				{
					_hasSpeciesMassFractions = true;
					return;
				}

				for(int i = 0; i < _speciesMassFractions.Length; ++i)
				{
					_speciesMassFractions[i] = value[i];
				}

				_hasSpeciesMassFractions = true;
				OnSpeciesMassFractionsChanged(); 
			}
		}

				
		public ReadOnlyArray<double> SpeciesMoleFractions
		{
			[DebuggerStepThrough]
			get
			{
				if(_speciesMoleFractions == null)
				{
					_speciesMoleFractions = new double[_model.NumberOfSpecies];
					_hasSpeciesMoleFractions = false;
				}

				if(!_hasSpeciesMoleFractions)
				{
					this.CalculateSpeciesMoleFractions(_speciesMoleFractions);
					_hasSpeciesMoleFractions = true;
				}

				return _speciesMoleFractions;
			}
			[DebuggerStepThrough]
			private set
			{
				if(_speciesMoleFractions == null)
				{
					_speciesMoleFractions = new double[_model.NumberOfSpecies];					
				}
				else if(_speciesMoleFractions == value.Data)
				{
					_hasSpeciesMoleFractions = true;
					return;
				}

				for(int i = 0; i < _speciesMoleFractions.Length; ++i)
				{
					_speciesMoleFractions[i] = value[i];
				}

				_hasSpeciesMoleFractions = true;
				OnSpeciesMoleFractionsChanged(); 
			}
		}

		 
	}
	
}
	