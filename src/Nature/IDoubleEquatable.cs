namespace Nature
{
    public interface IDoubleEquatable<T>
    {
        bool Equals(T other, DoubleComparer comparer);
    }
}
