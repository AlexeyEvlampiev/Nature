using System;

namespace Nature.Text
{
    using System.Text.RegularExpressions;
    using System.Threading;

    public class Markup : IMarkup
    {
        private readonly Lazy<string> _adaptedText;
        private readonly Lazy<TextPosition[]> _positions;

        protected Markup(string id, string text)
        {
            this.Id = id;
            this.OriginalText = text;
            _adaptedText = new Lazy<string>(BuildAdaptedTextWithCheck, LazyThreadSafetyMode.PublicationOnly);
            _positions = new Lazy<TextPosition[]>(BuildPositionsArray, LazyThreadSafetyMode.PublicationOnly);            
        }

        private string BuildAdaptedTextWithCheck()
        {
            string text = BuildAdaptedText();
            if (text.Length != OriginalText.Length)
                throw new InvalidOperationException();
            return text;
        }

        private TextPosition[] BuildPositionsArray() => TextPosition.Get(OriginalText);

        protected  virtual string BuildAdaptedText()
        {
            string text = OriginalText;
            if (Environment.NewLine == "\r\n")
            {
                text = Regex.Replace(text, @"(?s-m)(?<!\r)(?=\n)", "\r");
            }
            return text;
        }

        public string Id { get; }
        public string OriginalText { get; }
        public string AdaptedText => _adaptedText.Value;

        public TextPosition this[int i] => _positions.Value[i];
    }
}
