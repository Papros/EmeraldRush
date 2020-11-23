using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.GameModel
{
    class Adventurer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Chest { get; private set; }
        public int Pocket { get; private set; }
        public string uid { get; private set; }
        public PlayerStatus Status { get; private set; }

        public Adventurer(int id, string name = "Adventurer", int chest = 0, int pocket = 0){
            this.Id = id;
            this.Name = name;
            this.Chest = chest;
            this.Pocket = pocket;
            this.uid = "";
        }

    }
}
