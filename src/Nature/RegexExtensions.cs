namespace Nature
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    static class RegexExtensions
    {
        [DebuggerNonUserCode]
        public static Match Match(this Regex self, string text, Capture capture)
        {
            return self.Match(text, capture.Index, capture.Length);
        }
    }
}
