namespace Nature
{
    using System;
    using System.Diagnostics;

    public static class Disposable
    {
        [DebuggerNonUserCode]
        public static IDisposable FromCallback(Action dispose)
        {
            return new RelayDisposable(dispose ?? throw new ArgumentNullException(nameof(dispose)));
        }

        [DebuggerNonUserCode]
        public static IDisposable FromCallback(Action<object> dispose, object state)
        {
            return new RelayDisposable2(dispose ?? throw new ArgumentNullException(nameof(dispose)), state);
        }

        class RelayDisposable : IDisposable
        {
            private readonly Action _dispose;

            public RelayDisposable(Action dispose)
            {
                _dispose = dispose;
            }

            [DebuggerStepThrough]
            public void Dispose()
            {
                _dispose.Invoke();
            }
        }

        class RelayDisposable2 : IDisposable
        {
            private readonly Action<object> _dispose;
            private readonly object _state;

            public RelayDisposable2(Action<object> dispose, object state)
            {
                _dispose = dispose;
                _state = state;
            }

            [DebuggerStepThrough]
            public void Dispose()
            {
                _dispose.Invoke(_state);
            }
        }
    }
}
