using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class TitleTrophyGroups : TrophyGroups
    {
        public TrophyCount definedTrophies { get; set; }
        public List<TitleTrophyGroup> trophyGroups { get; set; }
        public string trophyTitleDetail { get; set; }
        public string trophyTitleIconUrl { get; set; }
        public string trophyTitleName { get; set; }
        public string trophyTitlePlatform { get; set; }
    }
}
