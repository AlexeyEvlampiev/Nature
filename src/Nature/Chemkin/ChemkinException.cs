namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ChemkinException : Exception
    {
        readonly string[] _messages;

        private ChemkinException(IEnumerable<string> messages)
        {
            _messages = messages.ToArray();
        }

        internal protected ChemkinException(Capture capture, ChemkinMarkup markup, string message)
            : this(capture.Index, markup, message)
        {
        }

        internal protected ChemkinException(int index, ChemkinMarkup markup, string message)
        {
            var position = markup[index];
            _messages = new string[]
            {
                $"{markup.Id}{position}: {message}"
            };
        }



        internal static ChemkinException Aggregate(IEnumerable<ChemkinException> exceptions)
        {
            return new ChemkinException(
                    from ex in exceptions
                    from msg in ex.AllMessages
                    select msg
                );
        }

        public override string Message => string.Join("; ", _messages);

        public IReadOnlyList<string> AllMessages => _messages;


    }
}
