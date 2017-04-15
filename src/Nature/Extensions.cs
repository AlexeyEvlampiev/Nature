namespace Nature
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    static class Extensions
    {
        [DebuggerNonUserCode]
        public static Match Match(this Regex self, string text, Capture capture)
        {
            return self.Match(text, capture.Index, capture.Length);
        }

        [DebuggerNonUserCode]
        public static bool Equals(this double self, double other, DoubleComparer comparer)
        {
            return comparer.Equals(self, other);
        }

    }
}
