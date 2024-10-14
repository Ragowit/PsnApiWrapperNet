using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class TrophyTitles
    {
        public int nextOffset { get; set; }
        public int totalItemCount { get; set; }
        public List<TrophyTitle> trophyTitles { get; set; }
    }
}
