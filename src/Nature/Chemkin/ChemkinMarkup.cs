namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class ChemkinMarkup
    {        
        readonly Lazy<TextPosition[]> _positions;

        public static async Task<ChemkinMarkup> CreateAsync(string resource)
        {
            if (File.Exists(resource))
            {
                var info = new FileInfo(resource);
                using (var reader = info.OpenText())
                {
                    string markup = await reader.ReadToEndAsync().ConfigureAwait(false);
                    return new ChemkinMarkup(info.Name, markup);
                }
            }
            else if (Uri.IsWellFormedUriString(resource, UriKind.Absolute))
            {
                var uri = new Uri(resource);
                var httpClient = new HttpClient();
                string markup = await httpClient.GetStringAsync(resource).ConfigureAwait(false);
                return new ChemkinMarkup(uri.LocalPath, markup);
            }
            else
            {
                return new ChemkinMarkup(resource);
            }
        }

        public IEnumerable<object> Parse()
        {
            var heap = new List<object>();
            var context = new DefaultDeserializationContext();
            var regex = new Regex(RegexUtils.MinifyStrict(@"
                \s* 
                    (?<thermo>\bTHERMO?\b.*)
            "), RegexOptions.Singleline);
            var match = regex.Match(AdaptedText);
            foreach (Capture capture in match.Captures)
            {
                var collection = Thermo.ThermoCollection.Parse(this, context, capture);
                heap.AddRange(collection);
            }

            return heap;
        }

        public ChemkinMarkup(string text) 
            : this($"text:{Guid.NewGuid()}", text)
        {
        }
        
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
