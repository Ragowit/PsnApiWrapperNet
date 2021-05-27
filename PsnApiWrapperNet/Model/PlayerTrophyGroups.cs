using System;
using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class PlayerTrophyGroups : TrophyGroups
    {
        public TrophyCount earnedTrophies { get; set; }
        public bool hiddenFlag { get; set; }
        public DateTime lastUpdatedDateTime { get; set; }
        public int progress { get; set; }
        public List<PlayerTrophyGroup> trophyGroups { get; set; }
    }
}
