using EmeraldRush.Model.GameEnum;

namespace EmeraldRush.Model.FirebaseModel
{
    class DecisionToken
    {
        public int Decision { get; set; }

        public DecisionToken(PlayerDecision decision)
        {
            this.Decision = (int)decision;
        }

    }
}
