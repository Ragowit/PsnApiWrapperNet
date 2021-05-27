using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Title
    {
        public string npTitleId { get; set; }
        public List<TrophyTitle> trophyTitles { get; set; }
    }
}
