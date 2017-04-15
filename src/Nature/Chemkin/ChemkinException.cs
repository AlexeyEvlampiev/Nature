namespace Nature.Chemkin
{
    using System;
    using System.Text.RegularExpressions;

    public abstract class ChemkinException : Exception
    {

        internal protected ChemkinException()
        { }

        internal protected ChemkinException(Capture capture, ChemkinMarkup markup, string message)
            : this(capture.Index, markup, message)
        {
        }

        internal protected ChemkinException(int index, ChemkinMarkup markup, string message) 
            : base(BuildMessage(index, markup, message))
        {
            Position = markup[index];
        }

        protected static string BuildMessage(int index, ChemkinMarkup markup, string message)
        {
            var position = markup[index];
            return $"{markup.Id}{position}: {message}";
        }


        internal TextPosition? Position { get; }
    }
}
