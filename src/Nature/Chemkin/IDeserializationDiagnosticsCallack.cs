using System.Text.RegularExpressions;

namespace Nature.Chemkin
{
    public interface IDeserializationDiagnosticsCallack
    {
        void Warning(Capture capture, ChemkinMarkup markup, string message);
        void Error(Capture capture, ChemkinMarkup markup, string message);
    }
}
