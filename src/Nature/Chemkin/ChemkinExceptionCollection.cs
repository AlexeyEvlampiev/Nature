namespace Nature.Chemkin
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    class ChemkinExceptionCollection : Collection<ChemkinException>
    {
        [DebuggerNonUserCode]
        public void ThrowIfNotEmpty()
        {
            if (Count == 1)
                throw this[0];
            if (Count > 0)
            {
                throw new ChemkinAggregateException(this);
            }
        }

        [DebuggerStepThrough]
        public void TryCatch(Action action)
        {
            try
            {
                action();
            }
            catch (ChemkinException ex)
            {
                this.Add(ex);
            }
        }
    }
}
