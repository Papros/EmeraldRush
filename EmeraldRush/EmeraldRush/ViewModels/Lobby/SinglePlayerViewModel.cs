using EmeraldRush.Model.AIMode.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.ViewModels.Lobby
{
    class SinglePlayerViewModel : BaseViewModel
    {

        private int decisionTime;
        public int DecisionTime
        {
            get { return decisionTime; }
            set { SetProperty(ref decisionTime, value); }
        }

        private int dragonsDeep;
        public int DragonsDeep
        {
            get { return dragonsDeep; }
            set {
                this.DragonDeepText = (value < 0 ? "NO DRAGON" : DragonsDeep.ToString());
                SetProperty(ref dragonsDeep, value);
            }
        }

        private int roundNumber;
        public int RoundNumber
        {
            get { return roundNumber; }
            set { SetProperty(ref roundNumber, value); }
        }

        private List<AIPlayer> bots;

        public List<AIPlayer> Bots
        {
            get { return bots; }
            set { 
                SetProperty(ref bots, value);
                this.PlayerNumber = bots.Count + " / " + maxPlayernumber;
            }
        }

        public SinglePlayerViewModel()
        {
            SetDefault();
        }

        private string playerNumber;
        public string PlayerNumber
        {
            get { return playerNumber; }
            set { SetProperty(ref playerNumber, value); }
        }

        private string dragonDeepText;
        public string DragonDeepText
        {
            get { return dragonDeepText; }
            set { SetProperty(ref dragonDeepText, value); }
        }

        public void AddAIPlayer()
        {
            bots.Add(new AIPlayer("Bob", 5, GameStyle.COWARDLY));
            this.PlayerNumber = bots.Count + " / " + maxPlayernumber;
        }

        public SinglePlayerViewModel StartGame()
        {
            return null;
        }

        public double minDragonsDeep = -1;
        public double maxDragonsDeep = 30;
        public double minRoundNumber = 1;
        public double maxRoundNumber = 5;
        public double minDecisionTime = 10;
        public double maxDecisiontime = 60;
        private int minPlayerNumber = 1;
        private int maxPlayernumber = 8;

        private void SetDefault()
        {
            this.DecisionTime = 15;
            this.DragonsDeep = 5;
            this.RoundNumber = 3;
            this.bots = new List<AIPlayer>();
            AddAIPlayer();
            AddAIPlayer();
            AddAIPlayer();
        }
    }
}
