namespace Nature.GMix
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot("SingleRootTestCases")]
    public class SingleRootTestCases
    {
        public class TestCase
        {
            [XmlElement("Description")]
            public string Description { get; set; }

            [XmlElement("Script")]
            public string Script { get; set; }


        }
    }
}
