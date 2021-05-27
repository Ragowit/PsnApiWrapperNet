using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class DomainResponse
    {
        public string domain { get; set; }
        public List<string> facetOptions { get; set; }
        public string next { get; set; }
        public List<SearchResult> results { get; set; }
        public int totalResultCount { get; set; }
        public string univexId { get; set; }
        public bool zeroState { get; set; }
    }
}
