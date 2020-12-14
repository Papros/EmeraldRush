using EmeraldRush.Model.GameEnum;
using System.Collections.Generic;

namespace EmeraldRush.Model.GameModel
{
    class Mine
    {
        public int EmeraldsForTake { get; set; }
        public int LastMoveTimestamp { get; set; }
        public MineStatus MineState { get; set; }
        public List<int> Node { get; set; }

        public Mine( )
        {
            this.MineState = MineStatus.NOT_VISITED;
            this.Node = new List<int>();
            this.EmeraldsForTake = 0;
            this.LastMoveTimestamp = 0;
        }

    }
}
