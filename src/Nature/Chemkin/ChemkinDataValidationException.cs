namespace Nature.Chemkin
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    public sealed class ChemkinDataValidationException : ChemkinException
    {
        internal ChemkinDataValidationException(Capture capture, ChemkinMarkup markup, string message)
            : base(capture, markup, message)
        {
        }
    }
}
