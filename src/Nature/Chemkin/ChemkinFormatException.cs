namespace Nature.Chemkin
{
    using System;
    using System.Text.RegularExpressions;

    public sealed class ChemkinFormatException : FormatException
    {
        readonly TextPosition _position;
        readonly string _sourceId;

        internal ChemkinFormatException(Capture capture, ChemkinMarkup markup, string message)
            : this(capture.Index, markup, message)
        {            
        }

        internal ChemkinFormatException(int index, ChemkinMarkup markup, string message) 
            : base(message)
        {
            _position = markup[index];
            _sourceId = markup.Id;
        }
        
        public override string Message => $"{_sourceId}{_position}: {base.Message}";

    }
}
