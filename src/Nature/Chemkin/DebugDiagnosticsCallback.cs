namespace Nature.Chemkin
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using Text;

    public class DebugDiagnosticsCallback : IDeserializationDiagnosticsCallback
    {
        public void Error(Capture capture, IMarkup markup, string message)
        {
            var position = markup[capture.Index];
            message = $"{markup.Id}{position}: {message}";
            Debug.Write(message);
        }

        public void Warning(Capture capture, IMarkup markup, string message)
        {
            var position = markup[capture.Index];
            message = $"{markup.Id}{position}: {message}";
            Debug.Write(message);
        }


    }
}
