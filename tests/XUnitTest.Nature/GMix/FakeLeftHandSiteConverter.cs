namespace Nature.GMix
{
    class FakeLeftHandSiteConverter : LeftHandSiteConverter
    {
        readonly bool _isSpeciesCodeResponse;

        public FakeLeftHandSiteConverter(bool isSpeciesCodeResponse)
        {
            _isSpeciesCodeResponse = isSpeciesCodeResponse;
        }
        protected override bool IsSpeciesCode(string expression) => _isSpeciesCodeResponse;
    }
}
