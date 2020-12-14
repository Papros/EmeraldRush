using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.AILogic
{
    class BraveDecisionStrategy : IDecisionStrategy
    {
        public int Difficult { get; set; }

        public BraveDecisionStrategy(int diff)
        {
            this.Difficult = diff;
        }

        public PlayerDecision makeDecision(GameInstance gameInstance, int playerID)
        {
            return PlayerDecision.GO_FURTHER;
        }
    }
}
