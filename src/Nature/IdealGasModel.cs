namespace Nature
{
    using Nature.Common;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class IdealGasModel
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly int _numberOfSpecies;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly string[] _speciesCodes;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly double[] _speciesMolarMasses;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly IReadOnlyDictionary<string, int> _ixSpeciesOrdynalByCode;

        public IdealGasModel(IEnumerable<object> objectHeap)
        {
            var objectList = objectHeap.ToList();
            try
            {                
                var speciesCodes = objectList.OfType<SpeciesCodesSet>().SingleOrDefault();
                if (ReferenceEquals(speciesCodes, null))
                {
                    var items = objectList.OfType<IChemicalSpeciesInfo>();
                    speciesCodes = new SpeciesCodesSet(items.Select(i=> i.SpeciesCode));
                }
                _speciesCodes = speciesCodes.Select(c => c.ToUpper()).ToArray();
                _numberOfSpecies = _speciesCodes.Length;
                _speciesMolarMasses = new double[_numberOfSpecies];
            }
            catch (Exception)
            {
                throw;
            }

            _ixSpeciesOrdynalByCode = _speciesCodes
                .ToDictionary(i => i, i => Array.IndexOf(_speciesCodes, i), StringComparer.OrdinalIgnoreCase);


            var elementMassRepository =
                objectList.OfType<IChemicalElementMassRepository>().SingleOrDefault() ?? 
                new ChemicalElementMassRepository();

            var chemicalFormulas = objectList.OfType<IChemicalFormula>().ToLookupBySpeciesCode();

            foreach (var spCode in _speciesCodes)
            {
                if (chemicalFormulas.ContainsKey(spCode))
                {
                    var formula = chemicalFormulas[spCode];
                    double molarMass = formula.CalculateMolarMass(elementMassRepository);
                    int speciesOrdinal = _ixSpeciesOrdynalByCode[spCode];
                    _speciesMolarMasses[speciesOrdinal] = molarMass;
                }
            }

        }

        public int NumberOfSpecies => this._numberOfSpecies;

        public ReadOnlyArray<string> SpeciesCodes => _speciesCodes;

        public ReadOnlyArray<double> SpeciesMolarMasses => _speciesMolarMasses;

        public int GetSpeciesOrdinal(string speciesCode) => _ixSpeciesOrdynalByCode[speciesCode];

        public bool IsSpeciesCode(string speciesCode) => _ixSpeciesOrdynalByCode.ContainsKey(speciesCode);


        public T[] AllocateSpeciesArray<T>() => new T[NumberOfSpecies];

        public void SetSpecies<T>(string speciesCode, T[] array, T value)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (speciesCode == null)
                throw new ArgumentNullException(nameof(speciesCode));
            if (array.Length != _numberOfSpecies)
                throw new ArgumentException();
            int ordinal = _ixSpeciesOrdynalByCode[speciesCode];
            array[ordinal] = value;
        }

        public T GetSpecies<T>(string speciesCode, ReadOnlyArray<T> array)
        {
            if (array.Data == null)
                throw new ArgumentNullException(nameof(array));
            if (speciesCode == null)
                throw new ArgumentNullException(nameof(speciesCode));
            int ordinal = _ixSpeciesOrdynalByCode[speciesCode];
            return array[ordinal];
        }
    }
}
