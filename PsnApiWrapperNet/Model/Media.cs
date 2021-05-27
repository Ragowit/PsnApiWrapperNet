using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Media
    {
        public List<object> audios { get; set; }
        public List<Image> images { get; set; }
        public List<Video> videos { get; set; }
    }
}
