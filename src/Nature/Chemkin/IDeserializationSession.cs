namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDeserializationSession
    {
        T GetOrCreate<T>(string key, Func<T> factory);
    }
}
