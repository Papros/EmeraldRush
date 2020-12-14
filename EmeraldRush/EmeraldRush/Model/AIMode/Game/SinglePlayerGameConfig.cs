using EmeraldRush.Model.AIMode.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.Game
{
    class SinglePlayerGameConfig
    {
        public int DragonsMinimalDeep { get; set; }
        public int DecisionTime { get; set; }
        public int MineNumber { get; set; }
        public AIPlayer[] botList { get; set; }
        public string PlayerNickname { get; set; }
    }
}
