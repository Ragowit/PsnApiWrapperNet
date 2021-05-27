using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Concept
    {
        public List<string> genres { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<string> subGenres { get; set; }
        public List<object> titleIds { get; set; }
    }
}
