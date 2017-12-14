namespace Nature.Text
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class DeserializationContext
    {
        readonly Stack<object> _context = new Stack<object>();
        readonly IDictionary<string, object> _data = new Dictionary<string, object>();

        public IDisposable Push(object item)
        {
            _context.Push(item);
            return Disposable.FromCallback(Pop, item);
        }

        public T FistOrDefault<T>() => _context.OfType<T>().FirstOrDefault();

        public T FistOrDefault<T>(Func<T, bool> filter) => _context.OfType<T>().Where(filter).FirstOrDefault();

        public T GetOrCreate<T>(Func<T> factory) => GetOrCreate(typeof(T).FullName, factory);

        public T GetOrCreate<T>(string key, Func<T> factory)
        {
            if (ReferenceEquals(_data, null))
                return factory();

            object value = null;
            if (_data.TryGetValue(key, out value) && value is T)
            {
                return (T)value;
            }

            _data[key] = value = factory();
            return (T)value;
        }


        private void Pop(object item)
        {
            var top = _context.Pop();
            if(ReferenceEquals(top, item) == false)
                throw new InvalidOperationException();

        }
    }
}
