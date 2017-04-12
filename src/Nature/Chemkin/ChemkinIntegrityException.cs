using System.Text.RegularExpressions;

namespace Nature.Chemkin
{
    public class ChemkinIntegrityException : ChemkinException
    {
        public ChemkinIntegrityException(Capture capture, ChemkinMarkup markup, string message)
            : base(capture, markup, message)
        {
        }
    }
}
