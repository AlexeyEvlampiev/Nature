using System.Diagnostics;

namespace Nature
{
    public partial struct ReadOnlyIdealGasState
    {
        public IdealGasModel Model => _innerState.Model;

        [DebuggerNonUserCode]
        public double SpeciesMoleFraction(string speciesCode) => _innerState.SpeciesMoleFraction(speciesCode);
    }
}
