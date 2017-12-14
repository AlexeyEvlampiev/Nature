namespace Nature.Text
{
    public interface IMarkup
    {        
        string Id { get; }
        string OriginalText { get; }
        string AdaptedText { get; }
        TextPosition this[int i] { get; }
    }
}