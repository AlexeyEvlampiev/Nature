namespace Nature.Text
{
    using System;
    using System.Diagnostics;
    using Chemkin;

    abstract class Parser<T> where T : class 
    {
        protected DeserializationContext Context { get; }
        protected IDeserializationDiagnosticsCallback DiagnosticsCallback => _diagnosticsCallback.Value;

        private readonly Lazy<T> _obj;
        private readonly Lazy<IDeserializationDiagnosticsCallback> _diagnosticsCallback;

        protected Parser(
            DeserializationContext context)
        {
            Context = context;
            _diagnosticsCallback = new Lazy<IDeserializationDiagnosticsCallback>(GetDiagnosticsCallback);
            _obj = new Lazy<T>(Parse);
        }

        protected virtual IDeserializationDiagnosticsCallback GetDiagnosticsCallback()
        {
            return Context.GetOrCreate<IDeserializationDiagnosticsCallback>(()=> new DebugDiagnosticsCallback());
        }

        public T Result => _obj.Value;

        protected abstract T CreateInstance();
        protected abstract void Parse(T instance);        

        private T Parse()
        {
            var result = CreateInstance();
            using (Context.Push(result))
            {
                Debug.Assert(ReferenceEquals(result, Context.FistOrDefault<T>()));
                Parse(result);
            }
            Debug.Assert(false == ReferenceEquals(result, Context.FistOrDefault<T>()));
            return result;
        }

        
    }
}
