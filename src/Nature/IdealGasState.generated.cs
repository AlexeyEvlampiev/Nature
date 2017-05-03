

namespace Nature
{
	using System.Diagnostics;

	public partial class IdealGasState
	{
		#region Private Fields 
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _temperature;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _pressure;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _molarMass;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _molarDensity;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _massDensity;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _reducedCp;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _reducedS;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double _speedOfSound;  
		 
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesMassFractions;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesMoleFractions;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesReducedCp;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesReducedH;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesReducedS;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] double[] _speciesLogX;  

		 
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasTemperature;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasPressure;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasMolarMass;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasMolarDensity;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasMassDensity;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasReducedCp;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasReducedS;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeedOfSound;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesMassFractions;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesMoleFractions;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesReducedCp;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesReducedH;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesReducedS;  
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] bool _hasSpeciesLogX;  
		#endregion		

		#region Partial Methods 
		partial void OnTemperatureChanged();  
		partial void OnPressureChanged();  
		partial void OnMolarMassChanged();  
		partial void OnMolarDensityChanged();  
		partial void OnMassDensityChanged();  
		partial void OnReducedCpChanged();  
		partial void OnReducedSChanged();  
		partial void OnSpeedOfSoundChanged();  
		partial void OnSpeciesMassFractionsChanged();  
		partial void OnSpeciesMoleFractionsChanged();  
		partial void OnSpeciesReducedCpChanged();  
		partial void OnSpeciesReducedHChanged();  
		partial void OnSpeciesReducedSChanged();  
		partial void OnSpeciesLogXChanged();  
		#endregion	

		private void DropState()
		{ 
			_hasTemperature = false;  
			_hasPressure = false;  
			_hasMolarMass = false;  
			_hasMolarDensity = false;  
			_hasMassDensity = false;  
			_hasReducedCp = false;  
			_hasReducedS = false;  
			_hasSpeedOfSound = false;  
			_hasSpeciesMassFractions = false;  
			_hasSpeciesMoleFractions = false;  
			_hasSpeciesReducedCp = false;  
			_hasSpeciesReducedH = false;  
			_hasSpeciesReducedS = false;  
			_hasSpeciesLogX = false;   
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
		 
		public double MolarDensity
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasMolarDensity)
					return _molarDensity;
				_molarDensity = CalculateMolarDensity();
				_hasMolarDensity = true;
				return _molarDensity;
			}
			[DebuggerStepThrough]
			private set
			{
				_molarDensity = value;
				_hasMolarDensity = true;
				OnMolarDensityChanged(); 
			}
		}
		 
		public double MassDensity
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasMassDensity)
					return _massDensity;
				_massDensity = CalculateMassDensity();
				_hasMassDensity = true;
				return _massDensity;
			}
			[DebuggerStepThrough]
			private set
			{
				_massDensity = value;
				_hasMassDensity = true;
				OnMassDensityChanged(); 
			}
		}
		 
		public double ReducedCp
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasReducedCp)
					return _reducedCp;
				_reducedCp = CalculateReducedCp();
				_hasReducedCp = true;
				return _reducedCp;
			}
			[DebuggerStepThrough]
			private set
			{
				_reducedCp = value;
				_hasReducedCp = true;
				OnReducedCpChanged(); 
			}
		}
		 
		public double ReducedS
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasReducedS)
					return _reducedS;
				_reducedS = CalculateReducedS();
				_hasReducedS = true;
				return _reducedS;
			}
			[DebuggerStepThrough]
			private set
			{
				_reducedS = value;
				_hasReducedS = true;
				OnReducedSChanged(); 
			}
		}
		 
		public double SpeedOfSound
		{
			[DebuggerStepThrough]
			get
			{
				if(_hasSpeedOfSound)
					return _speedOfSound;
				_speedOfSound = CalculateSpeedOfSound();
				_hasSpeedOfSound = true;
				return _speedOfSound;
			}
			[DebuggerStepThrough]
			private set
			{
				_speedOfSound = value;
				_hasSpeedOfSound = true;
				OnSpeedOfSoundChanged(); 
			}
		}
		 

				
		public ReadOnlyArray<double> SpeciesMassFractions
		{
			[DebuggerStepThrough]
			get
			{
				CalculateSpeciesMassFractions();
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
				CalculateSpeciesMoleFractions();
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

				
		public ReadOnlyArray<double> SpeciesReducedCp
		{
			[DebuggerStepThrough]
			get
			{
				CalculateSpeciesReducedCp();
				return _speciesReducedCp;
			}
			[DebuggerStepThrough]
			private set
			{
				if(_speciesReducedCp == null)
				{
					_speciesReducedCp = new double[_model.NumberOfSpecies];					
				}
				else if(_speciesReducedCp == value.Data)
				{
					_hasSpeciesReducedCp = true;
					return;
				}

				for(int i = 0; i < _speciesReducedCp.Length; ++i)
				{
					_speciesReducedCp[i] = value[i];
				}

				_hasSpeciesReducedCp = true;
				OnSpeciesReducedCpChanged(); 
			}
		}

				
		public ReadOnlyArray<double> SpeciesReducedH
		{
			[DebuggerStepThrough]
			get
			{
				CalculateSpeciesReducedH();
				return _speciesReducedH;
			}
			[DebuggerStepThrough]
			private set
			{
				if(_speciesReducedH == null)
				{
					_speciesReducedH = new double[_model.NumberOfSpecies];					
				}
				else if(_speciesReducedH == value.Data)
				{
					_hasSpeciesReducedH = true;
					return;
				}

				for(int i = 0; i < _speciesReducedH.Length; ++i)
				{
					_speciesReducedH[i] = value[i];
				}

				_hasSpeciesReducedH = true;
				OnSpeciesReducedHChanged(); 
			}
		}

				
		public ReadOnlyArray<double> SpeciesReducedS
		{
			[DebuggerStepThrough]
			get
			{
				CalculateSpeciesReducedS();
				return _speciesReducedS;
			}
			[DebuggerStepThrough]
			private set
			{
				if(_speciesReducedS == null)
				{
					_speciesReducedS = new double[_model.NumberOfSpecies];					
				}
				else if(_speciesReducedS == value.Data)
				{
					_hasSpeciesReducedS = true;
					return;
				}

				for(int i = 0; i < _speciesReducedS.Length; ++i)
				{
					_speciesReducedS[i] = value[i];
				}

				_hasSpeciesReducedS = true;
				OnSpeciesReducedSChanged(); 
			}
		}

				
		public ReadOnlyArray<double> SpeciesLogX
		{
			[DebuggerStepThrough]
			get
			{
				CalculateSpeciesLogX();
				return _speciesLogX;
			}
			[DebuggerStepThrough]
			private set
			{
				if(_speciesLogX == null)
				{
					_speciesLogX = new double[_model.NumberOfSpecies];					
				}
				else if(_speciesLogX == value.Data)
				{
					_hasSpeciesLogX = true;
					return;
				}

				for(int i = 0; i < _speciesLogX.Length; ++i)
				{
					_speciesLogX[i] = value[i];
				}

				_hasSpeciesLogX = true;
				OnSpeciesLogXChanged(); 
			}
		}

		 


		  
		private void CalculateSpeciesMassFractions()
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
		}
		  
		private void CalculateSpeciesMoleFractions()
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
		}
		  
		private void CalculateSpeciesReducedCp()
		{
			if(_speciesReducedCp == null)
			{
				_speciesReducedCp = new double[_model.NumberOfSpecies];
				_hasSpeciesReducedCp = false;
			}

			if(!_hasSpeciesReducedCp)
			{
				this.CalculateSpeciesReducedCp(_speciesReducedCp);
				_hasSpeciesReducedCp = true;
			}
		}
		  
		private void CalculateSpeciesReducedH()
		{
			if(_speciesReducedH == null)
			{
				_speciesReducedH = new double[_model.NumberOfSpecies];
				_hasSpeciesReducedH = false;
			}

			if(!_hasSpeciesReducedH)
			{
				this.CalculateSpeciesReducedH(_speciesReducedH);
				_hasSpeciesReducedH = true;
			}
		}
		  
		private void CalculateSpeciesReducedS()
		{
			if(_speciesReducedS == null)
			{
				_speciesReducedS = new double[_model.NumberOfSpecies];
				_hasSpeciesReducedS = false;
			}

			if(!_hasSpeciesReducedS)
			{
				this.CalculateSpeciesReducedS(_speciesReducedS);
				_hasSpeciesReducedS = true;
			}
		}
		  
		private void CalculateSpeciesLogX()
		{
			if(_speciesLogX == null)
			{
				_speciesLogX = new double[_model.NumberOfSpecies];
				_hasSpeciesLogX = false;
			}

			if(!_hasSpeciesLogX)
			{
				this.CalculateSpeciesLogX(_speciesLogX);
				_hasSpeciesLogX = true;
			}
		}
		 
	}	

	public partial struct ReadOnlyIdealGasState
	{
		internal readonly IdealGasState _innerState;

		public ReadOnlyIdealGasState(IdealGasState state) : this()
		{
			_innerState = state;
		}

		 
		/// <summary>
        /// 
        /// </summary>
		public double Temperature 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.Temperature; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double Pressure 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.Pressure; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double MolarMass 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.MolarMass; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double MolarDensity 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.MolarDensity; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double MassDensity 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.MassDensity; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double ReducedCp 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.ReducedCp; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double ReducedS 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.ReducedS; }
		}  
		/// <summary>
        /// 
        /// </summary>
		public double SpeedOfSound 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeedOfSound; }
		} 
		  
		/// <summary>
        /// 
        /// </summary>
		public ReadOnlyArray<double> SpeciesMassFractions 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeciesMassFractions; }
		}   
		/// <summary>
        /// 
        /// </summary>
		public ReadOnlyArray<double> SpeciesMoleFractions 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeciesMoleFractions; }
		}   
		/// <summary>
        /// 
        /// </summary>
		public ReadOnlyArray<double> SpeciesReducedCp 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeciesReducedCp; }
		}   
		/// <summary>
        /// 
        /// </summary>
		public ReadOnlyArray<double> SpeciesReducedH 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeciesReducedH; }
		}   
		/// <summary>
        /// 
        /// </summary>
		public ReadOnlyArray<double> SpeciesReducedS 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeciesReducedS; }
		}   
		/// <summary>
        /// 
        /// </summary>
		public ReadOnlyArray<double> SpeciesLogX 
		{ 
			[DebuggerStepThrough]
			get { return _innerState.SpeciesLogX; }
		} 
		public static implicit operator ReadOnlyIdealGasState(IdealGasState state) => new ReadOnlyIdealGasState(state);
	}
}
	