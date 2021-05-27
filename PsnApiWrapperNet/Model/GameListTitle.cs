using System;
using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class GameListTitle
    {
        public string category { get; set; }
        public Concept concept { get; set; }
        public DateTime firstPlayedDateTime { get; set; }
        public string imageUrl { get; set; }
        public DateTime lastPlayedDateTime { get; set; }
        public string localizedImageUrl { get; set; }
        public string localizedName { get; set; }
        public Media media { get; set; }
        public string name { get; set; }
        public int playCount { get; set; }
        public string playDuration { get; set; }
        public string service { get; set; }
        public List<object> stats { get; set; }
        public string titleId { get; set; }
    }
}
