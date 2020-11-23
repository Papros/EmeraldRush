using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.FirebaseModel
{
    class PlayersPublic
    {
        public int emotion { get; private set; }
        public string name { get; private set; }
        public int chest { get; private set; }
        public int pocket { get; private set; }
        public string uid { get; private set; }
        public PlayerStatus status { get; private set; }
        public int id { get; private set; }

        public PlayersPublic(int id, string name = "Adventurer", int chest = 0, int pocket = 0, string uid = "")
        {
            this.id = id;
            this.name = name;
            this.chest = chest;
            this.pocket = pocket;
            this.uid = uid;
            this.emotion = 0;
            this.status = PlayerStatus.EXPLORING;
            
        }
    }
}
