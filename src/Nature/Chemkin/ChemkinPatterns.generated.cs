namespace Nature.Chemkin
{
    static class ChemkinPatterns
    {
        public const string UnsignedRealNumber = @"(?:(?:(?:\d+\.?\d*)|(?:\.\d+))(?:[EDFedf][+-]?\d{1,3})?)";
        public const string SignedRealNumber = @"(?:[+-]?(?:(?:(?:(?:\d+\.?\d*)|(?:\.\d+))(?:[EDFedf][+-]?\d{1,3})?)))";
        public const string ArrheniusCorfficients = @"(?:(?<arrhenius1>(?:(?:(?:\d+\.?\d*)|(?:\.\d+))(?:[EDFedf][+-]?\d{1,3})?))\s*(?<arrhenius2>(?:[+-]?(?:(?:(?:(?:\d+\.?\d*)|(?:\.\d+))(?:[EDFedf][+-]?\d{1,3})?))))\s*(?<arrhenius3>(?:[+-]?(?:(?:(?:(?:\d+\.?\d*)|(?:\.\d+))(?:[EDFedf][+-]?\d{1,3})?)))))";
        public const string SpeciesId = @"(?:(?!\b(?:M|HV|LOW|TROE|SRI|REV)\b)(?:(?:[(][^\s()]*[)]|(?:\w[-,])+\w|\w)+\+*))";
        public const string ElementId = @"[a-zA-Z]{1,2}";
    }
}
