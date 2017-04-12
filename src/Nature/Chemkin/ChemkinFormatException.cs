namespace Nature.Chemkin
{
    using System.Text.RegularExpressions;

    public sealed class ChemkinFormatException : ChemkinException
    {
        internal ChemkinFormatException(Capture capture, ChemkinMarkup markup, string message)
            : base(capture.Index, markup, message)
        {            
        }

        internal ChemkinFormatException(int index, ChemkinMarkup markup, string message)
            : base(index, markup, message)
        {
        }
    }
}
