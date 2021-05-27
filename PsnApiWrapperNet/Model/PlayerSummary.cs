namespace PsnApiWrapperNet.Model
{
    public class PlayerSummary
    {
        public string accountId { get; set; }
        public TrophyCount earnedTrophies { get; set; }
        public int progress { get; set; }
        public int tier { get; set; }
        public int trophyLevel { get; set; }
    }
}
