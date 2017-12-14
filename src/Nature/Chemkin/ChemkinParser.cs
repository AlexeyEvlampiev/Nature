namespace Nature.Chemkin
{
    using Text;

    abstract class ChemkinParser<T> : Parser<T> where T : class
    {
        protected ChemkinMarkup Markup { get; }


        protected ChemkinParser(ChemkinMarkup markup, DeserializationContext context) 
            : base(context)
        {
            Markup = markup;
        }       
    }
}
