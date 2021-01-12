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
                case CardType.ENTRY: ImagePath = "card_tile_entry.png"; break;
                case CardType.DRAGON: ImagePath = "card_tile_dragon.png"; break;
                case CardType.EMERALDS: ImagePath = "card_tile_emerald.png"; break;
                case CardType.LAVA: ImagePath = "card_tile_lava.png"; break;
                case CardType.TOXIC: ImagePath = "card_tile_toxic.png"; break;
                case CardType.SPIDERS: ImagePath = "card_tile_spiders.png"; break;
                case CardType.SNAKES: ImagePath = "card_tile_snakes.png"; break;
                case CardType.TRAP: ImagePath = "card_tile_trap.png"; break;
                case CardType.ARTIFACT: ImagePath = "card_tile_emerald.png"; break;
            }
        }



    }
}
