using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.AIMode.AILogic
{
    interface IDecisionStrategy
    {
        int Difficult { get; set; }
        PlayerDecision MakeDecision(GameInstance gameInstance, int playerID);
    }
}
