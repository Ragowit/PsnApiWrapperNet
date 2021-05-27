using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class TrophyGroups
    {
        public TrophyCount definedTrophies { get; set; }
        public List<TrophyGroup> trophyGroups { get; set; }
        public string trophySetVersion { get; set; }
        public string trophyTitleDetail { get; set; }
        public string trophyTitleIconUrl { get; set; }
        public string trophyTitleName { get; set; }
        public string trophyTitlePlatform { get; set; }
    }
}
