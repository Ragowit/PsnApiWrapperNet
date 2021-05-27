namespace PsnApiWrapperNet.Model
{
    public class SearchResult
    {
        public string id { get; set; }
        public float score { get; set; }
        public SocialMetadata socialMetadata { get; set; }
        public string type { get; set; }
    }
}
