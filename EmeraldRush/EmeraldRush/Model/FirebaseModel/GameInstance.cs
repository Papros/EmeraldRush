using EmeraldRush.Model.GameModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.FirebaseModel
{
    class GameInstance
    {
        public string GameUID { get; set; }
        public int CurrentMineID { get; set; }
        public int MineNumber { get; set; }
        public Mine[] Mines { get; set; }

        public string[] PlayersActive { get; set; }
        public Object PlayersPrivate { get; set; }
        public PlayersPublic[] PlayersPublic { get; set; }

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
