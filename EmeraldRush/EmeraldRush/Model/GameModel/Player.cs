using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;

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
            chest = player.chest;
            emotion = player.emotion;
            name = player.name;
            pocket = player.pocket;
            status = player.status;

            switch (status)
            {
                case PlayerStatus.DEAD: statusSymbolPath = "deadSymbol.png"; break;
                case PlayerStatus.EXPLORING: statusSymbolPath = "exploringSymbol.png"; break;
                case PlayerStatus.RESTING: statusSymbolPath = "restingSymbol.png"; break;
            }
        }
    }
}
