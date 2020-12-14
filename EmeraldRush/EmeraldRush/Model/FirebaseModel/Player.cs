namespace EmeraldRush.Model.FirebaseModel
{
    class Player
    {
        public string UserUID { get; set; }
        public string Nickname { get; set; }
        public string GameUID { get; private set; }

        public Player(string game, string name, string userUid)
        {
            Nickname = name;
            GameUID = game;
            UserUID = userUid;
        }
    }
}
