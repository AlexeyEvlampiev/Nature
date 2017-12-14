namespace Nature.Text
{
    using System.Text.RegularExpressions;

    public interface IDeserializationDiagnosticsCallback
    {
        void Warning(Capture capture, IMarkup markup, string message);
        void Error(Capture capture, IMarkup markup, string message);
    }
}
