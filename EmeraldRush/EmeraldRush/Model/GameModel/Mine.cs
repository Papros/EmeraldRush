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

        public Mine()
        {
            MineState = MineStatus.NOT_VISITED;
            Node = new List<int>();
            EmeraldsForTake = 0;
            LastMoveTimestamp = 0;
        }

    }
}
