namespace Nature.Chemkin.Thermo
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NasaA7HeaderFormatOptions : INasaA7HeaderFormatOptions
    {
        public NasaA7HeaderFormatOptions()
        {
            SpeciesIdWidth = 18;
            DateWidth = 6;
            ElementWidth = 5;
            ElementsMaxCount = 4;
        }
        public int SpeciesIdWidth { get; set; }
        public int DateWidth { get; set; }

        public int ElementWidth { get; set; }

        public int ElementsMaxCount { get; set; }


        public static string BuildUrlParams(INasaA7HeaderFormatOptions options)
        {
            if (ReferenceEquals(options, null))
                throw new ArgumentNullException(nameof(options));
            return $"id-width={options.SpeciesIdWidth}&date-width={options.DateWidth}&elem-input-width={options.ElementWidth}&elem-max-count={options.ElementsMaxCount}";
        }
    }
}
