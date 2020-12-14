using EmeraldRush.Model.AIMode.AILogic;

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
            Name = name;
            Difficult = diff;
            Style = style;
        }
    }
}
