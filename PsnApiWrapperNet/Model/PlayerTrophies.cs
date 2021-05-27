using System;
using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class PlayerTrophies : Trophies
    {
        public DateTime lastUpdatedDateTime { get; set; }
        public List<PlayerTrophy> rarestTrophies { get; set; }
        public List<PlayerTrophy> trophies { get; set; }
    }
}
