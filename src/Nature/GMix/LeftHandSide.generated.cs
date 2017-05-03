

namespace Nature.GMix
{
	using System;
	using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

	

	abstract class LeftHandSiteConverter
	{
		readonly string _prefix; 
		readonly Regex _temperature = new Regex(@"^t(?:emp(?:er(?:ature)?)?)?$");  
		readonly Regex _pressure = new Regex(@"^p(?:res(?:sure)?)?$");  
		readonly Regex _equivalenceRatio = new Regex(@"^eq(?:\.?|uivalence)?\b\s*[-_]?\s*ratio$");  

		public LeftHandSiteConverter(string prefix = "$")
		{
			_prefix = prefix;
		}

		protected abstract bool IsSpeciesCode(string expression);

		public object Convert(string expression)
		{
			expression = expression.Trim();
			if(!expression.StartsWith(_prefix))
			{
				return IsSpeciesCode(expression)
					? (LeftHandSide)new PureSpeciesComponentLeftHandSide(expression)
					: (LeftHandSide)new ComplexComponentLeftHandSide(expression);
			}

			expression = expression.Substring(_prefix.Length);
			expression = Regex.Replace(expression, @"^\s*[(]\s*|\s*[)]\s*$", string.Empty).Trim();
			 
			if(_temperature.IsMatch(expression) )return new TemperatureLeftHandSide(expression);  
			if(_pressure.IsMatch(expression) )return new PressureLeftHandSide(expression);  
			if(_equivalenceRatio.IsMatch(expression) )return new EquivalenceRatioLeftHandSide(expression);  
			return new ComplexComponentDefinitionLeftHandSide(expression);			
		}

		sealed class DefaultConverter : LeftHandSiteConverter
		{
			readonly IdealGasModel _model;
			public DefaultConverter(IdealGasModel model)
			{
				_model = model;
			}

			protected override bool IsSpeciesCode(string code) => _model.IsSpeciesCode(code);
		}

		public static LeftHandSiteConverter FromIdealGasModel(IdealGasModel model) => new DefaultConverter(model);
	}

	abstract class LeftHandSide
	{
		public string Tag { get; }

		protected LeftHandSide(string tag) 
		{
			Tag = tag;
		}		
	}

	 
	sealed class PureSpeciesComponentLeftHandSide : LeftHandSide
	{
		public PureSpeciesComponentLeftHandSide(string tag) : base(tag) {}
	}
	 
	sealed class ComplexComponentLeftHandSide : LeftHandSide
	{
		public ComplexComponentLeftHandSide(string tag) : base(tag) {}
	}
	 
	sealed class ComplexComponentDefinitionLeftHandSide : LeftHandSide
	{
		public ComplexComponentDefinitionLeftHandSide(string tag) : base(tag) {}
	}
	 
	sealed class TemperatureLeftHandSide : LeftHandSide
	{
		public TemperatureLeftHandSide(string tag) : base(tag) {}
	}
	 
	sealed class PressureLeftHandSide : LeftHandSide
	{
		public PressureLeftHandSide(string tag) : base(tag) {}
	}
	 
	sealed class EquivalenceRatioLeftHandSide : LeftHandSide
	{
		public EquivalenceRatioLeftHandSide(string tag) : base(tag) {}
	}
	 

}
