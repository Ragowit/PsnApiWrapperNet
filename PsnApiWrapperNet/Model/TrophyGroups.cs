using System;
using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class TrophyGroups
    {
        public TrophyCount definedTrophies { get; set; }
        public TrophyCount earnedTrophies { get; set; }
        public bool? hiddenFlag { get; set; }
        public DateTime? lastUpdatedDateTime { get; set; }
        public string npCommunicationId { get; set; }
        public string npServiceName { get; set; }
        public int? progress { get; set; }
        public List<TrophyGroup> trophyGroups { get; set; }
        public string trophySetVersion { get; set; }
        public string trophyTitleDetail { get; set; }
        public string trophyTitleIconUrl { get; set; }
        public string trophyTitleName { get; set; }
        public string trophyTitlePlatform { get; set; }
    }
}
