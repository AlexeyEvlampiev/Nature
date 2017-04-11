using System;
using System.Text.RegularExpressions;

namespace Nature
{
    static class RegexUtils
    {
        internal static string Minify(string pattern)
        {
            return Regex.Replace(pattern, @"\s+", string.Empty);
        }

        internal static string MinifyStrict(string pattern)
        {
            pattern = Minify(pattern);
            return string.Format("{0}{1}{2}",
                pattern.StartsWith("^") ? string.Empty : "^",
                pattern,
                pattern.EndsWith("$") ? string.Empty : "$");
        }
    }
}
