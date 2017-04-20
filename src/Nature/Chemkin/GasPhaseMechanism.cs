namespace Nature.Chemkin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GasPhaseMechanism : IdealGasModel
    {
        private GasPhaseMechanism(IEnumerable<object> objectHeap) 
            : base(objectHeap)
        {
        }

        public static async Task<GasPhaseMechanism> CreateAsync(string resource, CancellationToken token)
        {
            var heap = await GetHeapAsync(resource, token);
            return new GasPhaseMechanism(heap);
        }

        public static async Task<GasPhaseMechanism> CreateAsync(
            IEnumerable<string> resources, 
            CancellationToken token)
        {
            var heap = new List<object>();
            var tasks =
                (from r in resources
                select GetHeapAsync(r, token)).ToList();
            foreach (var t in tasks)
            {
                heap.AddRange(await t.ConfigureAwait(false));
            }

            return new GasPhaseMechanism(heap);
        }

        private static async Task<IEnumerable<object>> GetHeapAsync(string resource, CancellationToken token)
        {
            var markup = await ChemkinMarkup.CreateAsync(resource).ConfigureAwait(false);
            token.ThrowIfCancellationRequested();
            return markup.Parse();
        }
    }
}
