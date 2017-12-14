namespace Nature.Text
{
    using System.Collections.Generic;

    public interface IMarkup
    {        
        string Id { get; }
        string OriginalText { get; }
        string AdaptedText { get; }
        TextPosition this[int i] { get; }
    }
}