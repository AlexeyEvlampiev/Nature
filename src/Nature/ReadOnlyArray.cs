namespace Nature
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents a read-only array of elements
    /// </summary>
    /// <typeparam name="T">The type of elements in the array</typeparam>
    [DebuggerDisplay("Length: {Length}")]
    public struct ReadOnlyArray<T> : IEnumerable<T>, IEquatable<ReadOnlyArray<T>>, IReadOnlyList<T>
    {
        #region Private Fields
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
        readonly private T[] m_array;
        #endregion

        /// <summary>
        /// Initializes a new object of the ReadOnlyArray(T) data type incapsulating the given array of values
        /// </summary>
        /// <param name="a">The type of elements in the array</param>
        [DebuggerStepThrough()]
        public ReadOnlyArray(T[] a)
        {
            m_array = a;
        }

        /// <summary>
        /// Implicitly converts the given array of values to the object of the ReadOnlyArray(T) type
        /// </summary>
        /// <param name="a">Array of values</param>
        /// <returns>ReadOnlyArray masking the given array of values</returns>
        [DebuggerStepThrough()]
        public static implicit operator ReadOnlyArray<T>(T[] a) => new ReadOnlyArray<T>(a);

        /// <summary>
        /// Gets the value of the element at a specific position in the ReadOnlyArray
        /// </summary>
        /// <param name="i">The zero-based index of the value to get.</param>
        /// <returns>The value of the element at position index.</returns>
        [DebuggerHidden()]
        public T this[int i] => m_array[i];

        /// <summary>
        /// Gets a 32-bit integer that represents the total number of elements in all the dimensions of the System.Array.
        /// </summary>
        [DebuggerHidden()]
        public int Length => m_array.Length;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal T[] Data => m_array;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        int IReadOnlyCollection<T>.Count => m_array.Length;

        /// <summary>
        /// Finds the index of the given array item
        /// </summary>
        /// <param name="item">Array item</param>
        /// <returns>Index of the array item</returns>
        public int IndexOf(T item) => Array.IndexOf(m_array, item);

        /// <summary>
        /// Finds the index of the given array item
        /// </summary>
        /// <param name="pred">Seach criteria</param>
        /// <returns>Index of the array item</returns>                
        public int IndexOf(Func<T, bool> pred)
        {
            T item = m_array.FirstOrDefault(i => pred(i));            
            return Array.IndexOf(m_array, item);
        }


        public bool Equals(ReadOnlyArray<T> other)
        {
            if (Length != other.Length)
                return false;
            if (ReferenceEquals(m_array, other.m_array))
                return true;
            for (int i = 0; i < m_array.Length; ++i)
            {
                if (m_array[i].Equals(other.m_array[i]) == false) { return false; }
            }
            return true;
        }


        public void CopyTo(ref T[] array)
        {
            T[] target = array;
            if (target == null || target.Length != m_array.Length)
            {
                array = target = new T[m_array.Length];
            }

            for (int i = 0; i < m_array.Length; ++i)
            {
                target[i] = m_array[i];
            }
        }

        public T[] ToArray()
        {
            var clone = new T[m_array.Length];
            for (int i = 0; i < clone.Length; ++i)
            {
                clone[i] = m_array[i];
            }

            return clone;
        }


        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an System.Collections.Generic.IEnumerator(T) for the ReadOnlyArray.
        /// </summary>
        /// <returns>An System.CollectionsGeneric.IEnumerator(T) for the ReadOnlyArray.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_array.Length; ++i)
            {
                yield return m_array[i];
            }
        }


        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an System.Collections.Generic.IEnumeratorfor the ReadOnlyArray.
        /// </summary>
        /// <returns>An System.Collections.Generic.IEnumerator for the ReadOnlyArray.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_array.GetEnumerator();
        }

        #endregion

    }   
}


