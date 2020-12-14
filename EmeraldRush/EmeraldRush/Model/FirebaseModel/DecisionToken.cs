using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.FirebaseModel
{
    class DecisionToken
    {
        public int Decision { get; set; }

        public DecisionToken(PlayerDecision decision)
        {
            Decision = (int)decision;
        }

    }
}
