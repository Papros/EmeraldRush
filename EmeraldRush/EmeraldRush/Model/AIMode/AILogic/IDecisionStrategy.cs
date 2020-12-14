using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.AILogic
{
    interface IDecisionStrategy
    {
        int Difficult { get; set; }
        PlayerDecision makeDecision(GameInstance gameInstance, int playerID);
    }
}
