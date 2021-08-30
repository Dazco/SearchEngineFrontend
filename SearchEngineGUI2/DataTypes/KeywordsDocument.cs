using System.Collections.Generic;

namespace SearchEngineGUI2.DataTypes
{
    public class KeywordsDocument
    {
        public string documentName { get; set; }
        public double documentLength { get; set; }
        public double documentRank { get; set; }
        public string documentLink { get; set; }

        public Dictionary<string, double> keywordsCount { get; set; }
    }
}