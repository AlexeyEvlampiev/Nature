﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Nature.Chemkin
{
    public class DefaultSession : IDeserializationSession
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        readonly IDictionary<string, object> _data;

        public DefaultSession()
        {
            _data = new Dictionary<string, object>();
        }

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
    }
}
