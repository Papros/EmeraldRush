using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.Player
{
    class Player
    {
        public int chest { get; private set; }
        public int id { get; private set; }
        public string name { get; private set; }
        public int pocket { get; private set; }
        public PlayerStatus status { get; private set; }
        public string uid { get; private set; }
        public bool IsExploring { get; set; }
        public bool Decision { get; set; }

        public Player(int id, string name = "Adventurer", int chest = 0, int pocket = 0, string uid = "")
        {
            this.id = id;
            this.name = name;
            this.chest = chest;
            this.pocket = pocket;
            this.uid = uid;
            this.status = PlayerStatus.EXPLORING;

        }
    }
}
