namespace PsnApiWrapperNet.Model
{
    public class TrophyCount
    {
        public int bronze { get; set; }
        public int gold { get; set; }
        public int platinum { get; set; }
        public int silver { get; set; }

        public int total => bronze + silver + gold + platinum;
    }
}
