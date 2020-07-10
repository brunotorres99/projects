using System.Collections.Generic;

namespace DX_Web_Challenge.Core.Criteria
{
    public class SearchResult <T> where T : class
    {
        public IEnumerable<T> Records { get; set; }
        public int RecordCount { get; set; }
    }
}