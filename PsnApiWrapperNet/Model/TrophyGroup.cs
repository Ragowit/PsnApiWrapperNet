using System;

namespace PsnApiWrapperNet.Model
{
    public class TrophyGroup
    {
        public TrophyCount definedTrophies { get; set; }
        public TrophyCount earnedTrophies { get; set; }
        public DateTime? lastUpdatedDateTime { get; set; }
        public int? progress { get; set; }
        public string trophyGroupDetail { get; set; }
        public string trophyGroupIconUrl { get; set; }
        public string trophyGroupId { get; set; }
        public string trophyGroupName { get; set; }
    }
}
