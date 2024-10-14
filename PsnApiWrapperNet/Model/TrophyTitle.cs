using System;
using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class TrophyTitle
    {
        public TrophyCount definedTrophies { get; set; }
        public TrophyCount earnedTrophies { get; set; }
        public bool hasTrophyGroups { get; set; }
        public bool hiddenFlag { get; set; } // Obsolete?
        public DateTime lastUpdatedDateTime { get; set; }
        public string npCommunicationId { get; set; }
        public string npServiceName { get; set; }
        public int progress { get; set; }
        public List<Trophy> rarestTrophies { get; set; }
        public int trophyGroupCount { get; set; }
        public string trophySetVersion { get; set; } // Obsolete?
        public string trophyTitleDetail { get; set; }
        public string trophyTitleIconUrl { get; set; }
        public string trophyTitleName { get; set; }
        public string trophyTitlePlatform { get; set; } // Obsolete?
    }
}
