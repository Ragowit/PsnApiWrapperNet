using System;

namespace PsnApiWrapperNet.Model
{
    public class PlayerTrophyGroup
    {
        public TrophyCount earnedTrophies { get; set; }
        public DateTime lastUpdatedDateTime { get; set; }
        public int progress { get; set; }
        public string trophyGroupId { get; set; }
    }
}
