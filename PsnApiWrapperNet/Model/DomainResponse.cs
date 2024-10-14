using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class DomainResponse
    {
        public string domain { get; set; }
        public string domainExpandedTitle { get; set; }
        public string domainTitle { get; set; }
        public List<string> domainTitleHighlight { get; set; }
        public string domainTitleMessageId { get; set; }
        public string next { get; set; }
        public List<SearchResult> results { get; set; }
        public int totalResultCount { get; set; }
        public bool zeroState { get; set; }
    }
}
