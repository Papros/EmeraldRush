using EmeraldRush.Model.AIMode.Player;

namespace EmeraldRush.Model.AIMode.Game
{
    class SinglePlayerGameConfig
    {
        public int DragonsMinimalDeep { get; set; }
        public int DecisionTime { get; set; }
        public int MineNumber { get; set; }
        public AIPlayer[] BotList { get; set; }
        public string PlayerNickname { get; set; }
    }
}
