using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngineGUI2.DataTypes
{
    class SearchResponse
    {
        public List<KeywordsDocument> documents { get; set; }
        public double queryTime { get; set; }
        public double rankingTime { get; set; }
    }
}
