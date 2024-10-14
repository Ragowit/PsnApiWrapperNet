using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class UniversalSearch
    {
        public List<DomainResponse> domainResponses { get; set; }
        public bool fallbackQueried { get; set; }
        public string prefix { get; set; }
        public QueryFrequency queryFrequency { get; set; }
        public List<ResponseStatus> responseStatus { get; set; }
        public StrandPaginationResponse strandPaginationResponse { get; set; }

        public SocialMetadata FirstResult() => domainResponses[0]?.results[0]?.socialMetadata;
    }
}
