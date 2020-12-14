using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.GameModel
{
    class Card
    {
        public int EmeraldValue { get; private set; }
        public CardType Type { get; private set; }
        public int CardID { get; private set; }

        public string ImagePath { get; private set; }
        public string Title { get; private set; }

        public Card(int cardID, CardType type, int emeraldsValue = 0)
        {
            EmeraldValue = emeraldsValue;
            CardID = cardID;
            Type = type;
            Title = (Type == CardType.ARTIFACT || Type == CardType.EMERALDS ? EmeraldValue + " " : "") + type.ToString();


            switch (type)
            {
                case CardType.ENTRY: ImagePath = "mine_entry.png"; break;
                case CardType.DRAGON: ImagePath = "dragon.png"; break;
                case CardType.EMERALDS: ImagePath = "cave.png"; break;
                case CardType.LAVA: ImagePath = "lava.png"; break;
                case CardType.ROCKS: ImagePath = "toxic.png"; break;
                case CardType.SPIDERS: ImagePath = "spiders.png"; break;
                case CardType.SNAKES: ImagePath = "snakes.png"; break;
                case CardType.TRAP: ImagePath = "trap.png"; break;
                case CardType.ARTIFACT: ImagePath = "cave.png"; break;
            }
        }



    }
}
