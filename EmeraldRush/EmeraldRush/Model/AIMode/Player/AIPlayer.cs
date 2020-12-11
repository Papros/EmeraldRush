using EmeraldRush.Model.AIMode.AILogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.Player
{
    class AIPlayer
    {
        public string Name { get; set; }
        public int Difficult { get; set; }
        public GameStyle Style { get; set; }
        public IDecisionStrategy DecisionStrategy;

        public AIPlayer(string name, int diff, GameStyle style)
        {
            this.Name = name;
            this.Difficult = diff;
            this.Style = style;
        }
    }
}
