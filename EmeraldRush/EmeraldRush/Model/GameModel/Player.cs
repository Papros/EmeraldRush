using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.GameModel
{
    class Player
    {
        public int chest { get; set; }
        public int emotion { get; set; }
        public string name { get; set; }
        public int pocket { get; set; }
        public PlayerStatus status { get; set; }
        public string statusSymbolPath { get; set; }

        public Player(PlayersPublic player)
        {
            this.chest = player.chest;
            this.emotion = player.emotion;
            this.name = player.name;
            this.pocket = player.pocket;
            this.status = player.status;

            switch (status)
            {
                case PlayerStatus.DEAD: this.statusSymbolPath = "deadSymbol.png";  break;
                case PlayerStatus.EXPLORING: this.statusSymbolPath = "exploringSymbol.png"; break;
                case PlayerStatus.RESTING: this.statusSymbolPath = "restingSymbol.png"; break;
            }
        }
    }
}
