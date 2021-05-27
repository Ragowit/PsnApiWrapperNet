using System;

namespace PsnApiWrapperNet.Model
{
    public class PlayerTrophy : Trophy
    {
        public bool earned { get; set; }
        public DateTime earnedDateTime { get; set; }
        public string progress { get; set; }
        public DateTime progressedDateTime { get; set; }
        public int progressRate { get; set; }
        public string trophyEarnedRate { get; set; }
        public int trophyRare { get; set; }
    }
}
