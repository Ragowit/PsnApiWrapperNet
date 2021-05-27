using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Trophies
    {
        public bool hasTrophyGroups { get; set; }
        public int totalItemCount { get; set; }
        public List<TitleTrophy> trophies { get; set; }
        public string trophySetVersion { get; set; }
    }
}
