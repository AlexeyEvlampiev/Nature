using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nature.Chemkin
{
    public class ChemkinMarkup
    {        
        readonly Lazy<TextPosition[]> _positions;

        public ChemkinMarkup(string text) 
            : this($"text:{Guid.NewGuid()}", text)
        { }
        
        public ChemkinMarkup(string id, string text)
        {
            this.Id = id;
            char space = Convert.ToChar(32);                      
            if (Environment.NewLine == "\r\n")
            {
                text = Regex.Replace(text, @"(?s-m)(?<!\r)(?=\n)", "\r");
            }
            OriginalText = text;
            AdaptedText = text.ToUpper();

            AdaptedText = Regex
                .Replace(AdaptedText,
                @"(?:(?!\r\n)(?!\n)\s|!.*)+", 
                m => new string(space, m.Length), RegexOptions.Multiline);

            if (AdaptedText.Length != OriginalText.Length)
                throw new InvalidOperationException();

            AdaptedText = Regex.Replace(AdaptedText, @"(?:\r?\n[^\S\n]*(?=\n))+", 
                m => new string(space, m.Length));

            if (AdaptedText.Length != OriginalText.Length)
                throw new InvalidOperationException();

            _positions = new Lazy<TextPosition[]>(() => TextPosition.Get(OriginalText));
        }

        public string Id { get; }

        internal TextPosition this[int i] => _positions.Value[i];

        public string OriginalText { get; }

        public string AdaptedText { get; }
    }
}
