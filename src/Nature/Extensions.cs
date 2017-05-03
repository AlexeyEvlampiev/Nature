namespace Nature
{
    using System;
    using System.Collections.Generic;
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


        public static bool Equals<TLhs, TRhs>(this TLhs lhs, TRhs rhs, DoubleComparer comparer)
            where TLhs : IReadOnlyList<double>
            where TRhs : IReadOnlyList<double>
        {
            if (lhs.Count != rhs.Count)
                return false;
            for (int i = 0; i < lhs.Count; ++i)
            {
                if (comparer.Equals(lhs[i], rhs[i]) == false) { return false; }
            }
            return true;
        }


        internal static T GetOrCreate<T>(this IDictionary<string, object> self, string key, Func<T> factory)
        {
            if (ReferenceEquals(self, null))
                return factory();

            object value = null;
            if (self.TryGetValue(key, out value) && value is T)
            {
                return (T)value;
            }

            self[key] = value = factory();
            return (T)value;
        }
    }
}
