namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChemkinAggregateException : ChemkinException
    {
        public ChemkinException[] InnerChemkinExceptions { get; }

        internal ChemkinAggregateException(IEnumerable<ChemkinException> innerExceptions)
        {
            var exceptions = new List<ChemkinException>();
            foreach (var ex in innerExceptions)
            {
                if (ex is ChemkinAggregateException)
                {
                    var aggregate = ex as ChemkinAggregateException;
                    foreach (var iex in aggregate.InnerChemkinExceptions)
                    {
                        exceptions.Add(iex);
                    }
                }
                else
                {
                    exceptions.Add(ex);
                }
            }

            InnerChemkinExceptions = exceptions.ToArray();
        }

        public override string Message => string.Join(Environment.NewLine, InnerChemkinExceptions.Select(iex=> iex.Message));
    }
}
