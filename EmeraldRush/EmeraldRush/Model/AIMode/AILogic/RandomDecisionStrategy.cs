using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using System;

namespace EmeraldRush.Model.AIMode.AILogic
{
    class RandomDecisionStrategy : IDecisionStrategy
    {
        public int Difficult { get; set; }

        public RandomDecisionStrategy(int diff)
        {
            Difficult = diff;
        }

        public PlayerDecision MakeDecision(GameInstance gameInstance, int playerID)
        {
            Random rand = new Random();
            bool decision = (rand.NextDouble() * 10 > 3);

            if (decision)
            {
                return PlayerDecision.GO_FURTHER;
            }
            else
            {
                return PlayerDecision.GO_BACK;
            }
        }
    }
}
