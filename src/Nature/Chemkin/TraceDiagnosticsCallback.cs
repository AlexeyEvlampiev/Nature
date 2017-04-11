using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Nature.Chemkin
{
    public class TraceDiagnosticsCallback : IDeserializationDiagnosticsCallack
    {
        public void Warning(Capture capture, ChemkinMarkup markup, string message)
        {
            var position = markup[capture.Index];
            message = $"{markup.Id}{position}: {message}";
            Debug.Write(message);
        }
    }
}
