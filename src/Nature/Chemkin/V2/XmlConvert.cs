namespace Nature.Chemkin.V2
{
    using System;
    using System.Collections.Concurrent;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Thermo;

    public partial class XmlConvert
    {
        private readonly INasaA7HeaderFormatOptions _nasaA7HeaderFormatOptions;
        private readonly IFormatInfo _formatInfo;
        readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        public XmlConvert() 
            : this(null, null)
        {
        }


        public XmlConvert(
            IFormatInfo formatInfo,
            INasaA7HeaderFormatOptions nasaA7HeaderFormatOptions)
        {
            _nasaA7HeaderFormatOptions = nasaA7HeaderFormatOptions ?? new NasaA7HeaderFormatOptions();
            _formatInfo = formatInfo ?? new DefaultFormatInfo();
        }

        private T GetOrCreate<T>(string key, Func<T> factory)
        {
            var value = (T)_cache.GetOrAdd(key, factory());
            return value;
        }

        private XElement XElement(string name, Capture capture, ChemkinMarkup markup, params object[] items)
        {
            var position = markup[capture.Index];
            return new XElement(name,
                new XAttribute("index", capture.Index),
                new XAttribute("length", capture.Length),
                new XAttribute("line", position.Line),
                new XAttribute("column", position.Column),
                items);
        }
    }
}
