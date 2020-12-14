using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.AIMode.AILogic
{
    class BraveDecisionStrategy : IDecisionStrategy
    {
        public int Difficult { get; set; }

        public BraveDecisionStrategy(int diff)
        {
            Difficult = diff;
        }

        public PlayerDecision MakeDecision(GameInstance gameInstance, int playerID)
        {
            return PlayerDecision.GO_FURTHER;
        }
    }
}
