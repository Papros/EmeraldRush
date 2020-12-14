using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.FirebaseModel
{
    class PlayersPublic
    {
        public int chest { get; set; }
        public int emotion { get; set; }
        public int id { get; set; }
        public string name { get;  set; }
        public int pocket { get; set; }
        public PlayerStatus status { get; set; }
        public string uid { get; set; }

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
