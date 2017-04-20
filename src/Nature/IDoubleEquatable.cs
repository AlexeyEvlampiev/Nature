namespace Nature
{
    public interface IDoubleEquatable<T>
    {
        bool IsMatch(T other, DoubleComparer comparer);
    }
}
