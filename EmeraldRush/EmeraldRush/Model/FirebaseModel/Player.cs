using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.FirebaseModel
{
    class Player
    {
        public string Nickname { get; set; }
        public string GameUID { get; private set; }

        public Player(string game, string name)
        {
            this.Nickname = name;
            this.GameUID = game;
        }
    }
}
