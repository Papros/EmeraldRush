using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.FirebaseModel
{
    class GameInstance
    {
        public int CurrentMineID { get; set; }
        public int DecisionTime { get; set; }
        public int DragonMinimalDeep { get; set; }
        public int GameStartTimestamp { get; set; }
        public string GameUID { get; set; }
        public int MineNumber { get; set; }
        public Mine[] Mines { get; set; }
        public PlayersPublic[] PlayersPublic { get; set; }
        public GameStatus PublicState { get; set; }
        public int RoundCooldownTime { get; set; }
        public int RoundID { get; set; }

        public GameInstance()
        {

        }

        public Mine GetCurrent()
        {
            if(Mines != null)
            {
                if (Mines.Length > CurrentMineID && CurrentMineID >= 0)
                {
                    return Mines[CurrentMineID];
                }
                else
                {
                    Console.WriteLine("Error in GetCurrent()");
                }
            }
            else
            {
                Console.WriteLine("Mines are null in GetCurrent()");
            }
                    

            return new Mine();
        }

        public PlayersPublic GetPlayerData(string playerUID)
        {
            if(PlayersPublic != null)
            for(int iter=0; iter< PlayersPublic.Length; iter++)
            {
                    if (PlayersPublic[iter].uid == playerUID) return PlayersPublic[iter];
            }

            return null;
        }

    }
}
