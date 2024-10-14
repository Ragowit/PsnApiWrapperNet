using System;

namespace PsnApiWrapperNet.Model
{
    public class Trophy
    {
        public bool? earned { get; set; }
        public DateTime? earnedDateTime { get; set; }
        public string progress { get; set; }
        public DateTime? progressedDateTime { get; set; }
        public int? progressRate { get; set; }
        public string trophyDetail { get; set; }
        public string trophyEarnedRate { get; set; }
        public string trophyGroupId { get; set; }
        public bool trophyHidden { get; set; }
        public string trophyIconUrl { get; set; }
        public int trophyId { get; set; }
        public string trophyName { get; set; }
        public string trophyProgressTargetValue { get; set; }
        public int? trophyRare { get; set; }
        public string trophyRewardImageUrl { get; set; }
        public string trophyRewardName { get; set; }
        public string trophyType { get; set; }
    }
}
