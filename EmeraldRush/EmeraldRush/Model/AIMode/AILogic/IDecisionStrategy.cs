using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.AILogic
{
    interface IDecisionStrategy
    {
        PlayerDecision makeDecision(SinglePlayerGameInstance gameInstance, int playerID);
    }
}
