using EmeraldRush.Model.GameEnum;
using System;

namespace EmeraldRush.Model.GameModel
{
    class CardDeck
    {
        public Card[] Cards { get; private set; }

        public CardDeck()
        {
            Cards = new Card[36];
            Cards[0] = new Card(0, CardType.LAVA);
            Cards[1] = new Card(1, CardType.LAVA);
            Cards[2] = new Card(2, CardType.LAVA);

            Cards[3] = new Card(3, CardType.ROCKS);
            Cards[4] = new Card(4, CardType.ROCKS);
            Cards[5] = new Card(5, CardType.ROCKS);

            Cards[6] = new Card(6, CardType.SNAKES);
            Cards[7] = new Card(7, CardType.SNAKES);
            Cards[8] = new Card(8, CardType.SNAKES);

            Cards[9] = new Card(9, CardType.SPIDERS);
            Cards[10] = new Card(10, CardType.SPIDERS);
            Cards[11] = new Card(11, CardType.SPIDERS);

            Cards[12] = new Card(12, CardType.TRAP);
            Cards[13] = new Card(13, CardType.TRAP);
            Cards[14] = new Card(14, CardType.TRAP);

            Cards[15] = new Card(15, CardType.DRAGON);

            Cards[16] = new Card(16, CardType.ARTIFACT,12);
            Cards[17] = new Card(17, CardType.ARTIFACT,10);
            Cards[18] = new Card(18, CardType.ARTIFACT,8);
            Cards[19] = new Card(19, CardType.ARTIFACT,7);
            Cards[20] = new Card(20, CardType.ARTIFACT,5);


            Cards[21] = new Card(21, CardType.EMERALDS, 1);
            Cards[22] = new Card(22, CardType.EMERALDS, 2);
            Cards[23] = new Card(23, CardType.EMERALDS, 3);
            Cards[24] = new Card(24, CardType.EMERALDS, 4);
            Cards[25] = new Card(25, CardType.EMERALDS, 5);

            Cards[26] = new Card(26, CardType.EMERALDS, 5);
            Cards[27] = new Card(27, CardType.EMERALDS, 7);
            Cards[28] = new Card(28, CardType.EMERALDS, 7);
            Cards[29] = new Card(29, CardType.EMERALDS, 9);
            Cards[30] = new Card(30, CardType.EMERALDS, 9);

            Cards[31] = new Card(31, CardType.EMERALDS, 11);
            Cards[32] = new Card(32, CardType.EMERALDS, 11);
            Cards[33] = new Card(33, CardType.EMERALDS, 14);
            Cards[34] = new Card(34, CardType.EMERALDS, 15);
            Cards[35] = new Card(35, CardType.EMERALDS, 17);

        }

        public Card GetCardOfID(int cardId)
        {
            return Cards[cardId];
        }

        public bool IsTrap(int index)
        {
            return Cards[index].Type != CardType.EMERALDS && Cards[index].Type != CardType.ARTIFACT;
        }

        public bool IsDragon(int index)
        {
            return Cards[index].Type == CardType.DRAGON;
        }

        public int EmeraldValue(int index)
        {
            return Cards[index].EmeraldValue;
        }

        public Card[] GetThisDeck(int[] nodes)
        {
            if(nodes != null)
            {
                Card[] resoultDeck = new Card[nodes.Length];

                for (int iter = 0; iter < nodes.Length; iter++)
                {
                    resoultDeck[iter] = Cards[nodes[iter]];
                    Console.WriteLine(nodes[iter] + ":> " + resoultDeck[iter].Title);
                }

                return resoultDeck;
            }
            else
            {
                Console.WriteLine("Card ids array null in GetThisDeck()");
                return new Card[0];
            }
            
        }

    }
}
