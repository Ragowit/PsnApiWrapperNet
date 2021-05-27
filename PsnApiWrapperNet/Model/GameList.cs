using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class GameList
    {
        public int nextOffset { get; set; }
        public int? previousOffset { get; set; }
        public List<GameListTitle> titles { get; set; }
        public int totalItemCount { get; set; }
    }
}
