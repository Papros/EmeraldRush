using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.AILogic
{
    class BraveDecisionStrategy : IDecisionStrategy
    {
        public PlayerDecision makeDecision(SinglePlayerGameInstance gameInstance, int playerID)
        {
            return PlayerDecision.GO_FURTHER;
        }
    }
}
