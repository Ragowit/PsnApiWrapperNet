using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class UniversalSearch
    {
        public List<DomainResponse> domainResponses { get; set; }
        public bool fallbackQueried { get; set; }
        public string prefix { get; set; }
        public List<object> suggestions { get; set; }

        public SocialMetadata FirstResult() => domainResponses[0]?.results[0]?.socialMetadata;
    }
}
