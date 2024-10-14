using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Concept
    {
        public string country { get; set; }
        public List<string> genres { get; set; }
        public int id { get; set; }
        public string language { get; set; }
        public LocalizedName localizedName { get; set; }
        public Media media { get; set; }
        public string name { get; set; }
        public List<string> titleIds { get; set; }
    }
}
