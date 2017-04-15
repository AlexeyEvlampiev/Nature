namespace Nature.Chemkin
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    public class DebugDiagnosticsCallback : IDeserializationDiagnosticsCallack
    {
        public void Error(Capture capture, ChemkinMarkup markup, string message)
        {
            var position = markup[capture.Index];
            message = $"{markup.Id}{position}: {message}";
            Debug.Write(message);
        }

        public void Warning(Capture capture, ChemkinMarkup markup, string message)
        {
            var position = markup[capture.Index];
            message = $"{markup.Id}{position}: {message}";
            Debug.Write(message);
        }


    }
}
