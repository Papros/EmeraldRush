using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.AILogic
{
    class RandomDecisionStrategy : IDecisionStrategy
    {
        public PlayerDecision makeDecision(SinglePlayerGameInstance gameInstance, int playerID)
        {
            Random rand = new Random();
            bool decision = ( rand.Next(0, 1) == 1);

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
