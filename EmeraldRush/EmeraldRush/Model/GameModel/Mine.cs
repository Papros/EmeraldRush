using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.GameModel
{
    class Mine
    {
        public int EmeraldsForTake { get; set; }
        public int LastMoveTimestamp { get; set; }
        public MineStatus MineState { get; set; }
        public int[] Node { get; set; }

        public Mine( )
        {
            this.MineState = MineStatus.NOT_VISITED; 
            this.Node = new int[0];
            this.EmeraldsForTake = 0;
            this.LastMoveTimestamp = 0;
        }

    }
}
