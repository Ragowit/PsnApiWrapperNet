using System;
using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Trophies
    {
        public bool hasTrophyGroups { get; set; }
        public DateTime lastUpdatedDateTime { get; set; }
        public List<Trophy> rarestTrophies { get; set; }
        public int totalItemCount { get; set; }
        public List<Trophy> trophies { get; set; }
        public string trophySetVersion { get; set; }
    }
}
