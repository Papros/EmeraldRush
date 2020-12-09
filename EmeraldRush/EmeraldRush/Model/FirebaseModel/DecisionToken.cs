using EmeraldRush.Model.GameEnum;
using System;
using System.Collections.Generic;
using System.Text;

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
