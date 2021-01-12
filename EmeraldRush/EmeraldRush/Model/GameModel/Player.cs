using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using System;

namespace EmeraldRush.Model.GameModel
{
    class Player : IComparable
    {
        public int chest { get; set; }
        public int emotion { get; set; }
        public string name { get; set; }
        public int pocket { get; set; }
        public PlayerStatus status { get; set; }
        public string statusSymbolPath { get; set; }

        public Player(PlayersPublic player)
        {
            chest = player.chest;
            emotion = player.emotion;
            name = player.name;
            pocket = player.pocket;
            status = player.status;

            switch (status)
            {
                case PlayerStatus.DEAD: statusSymbolPath = "player_status_dead.png"; break;
                case PlayerStatus.EXPLORING: statusSymbolPath = "player_status_exploring.png"; break;
                case PlayerStatus.RESTING: statusSymbolPath = "player_status_resting.png"; break;
            }
        }

        public int CompareTo(object obj)
        {
            return (obj as Player).chest.CompareTo(chest);
        }
    }
}
