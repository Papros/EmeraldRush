using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.AIMode.Player;
using EmeraldRush.Model.GameManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EmeraldRush.ViewModels.Lobby
{
    class SinglePlayerLobbyViewModel : BaseViewModel
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

        private ObservableCollection<AIPlayer> bots;

        public ObservableCollection<AIPlayer> Bots
        {
            get { return bots; }
            set {
                SetProperty(ref bots, value);
                this.PlayerNumber = bots.Count + " / " + maxPlayernumber;
            }
        }

        public SinglePlayerLobbyViewModel()
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
            if (bots.Count < maxPlayernumber)
            {
                Bots.Add(new AIPlayer("Bob", 5, GameStyle.RANDOM));
                this.PlayerNumber = bots.Count + " / " + maxPlayernumber;
            }
        }

        public void RemoveBot(object bot)
        {
            if (bots.Count > minPlayerNumber)
            {
                Console.WriteLine("Removed");
                Bots.Remove(bot as AIPlayer);
            }
            this.IsSelected = false;
            this.Selected = null;
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private int selectedDiff;
        public int SelectedDiff
        {
            get { return selectedDiff; }
            set { SetProperty(ref selectedDiff, value); }
        }

        private int styleIndex;
        public int StyleIndex
        {
            get { return styleIndex; }
            set { SetProperty(ref styleIndex, value); }
        }

        private string selectedName;
        public string SelectedName
        {
            get { return selectedName; }
            set { SetProperty(ref selectedName, value); }
        }

        private GameStyle selectedStyle;
        public GameStyle SelectedStyle
        {
            get { return selectedStyle; }
            set { SetProperty(ref selectedStyle, value); }
        }

        private AIPlayer selected;
        public AIPlayer Selected
        {
            get { return selected; }
            set
            {
                if(value != null)
                {
                    this.SelectedDiff = value.Difficult;
                    this.SelectedName = value.Name;
                    this.SelectedStyle = value.Style;
                    this.styleIndex = styleList.IndexOf(value.Style);
                    IsSelected = true;
                }
                SetProperty(ref selected, value);
            }
        }
        
        public void selectionDone()
        {

            int index = bots.IndexOf(Selected);

            if(index >=0 && index < bots.Count)
            {
                bots[index] = new AIPlayer(selectedName, selectedDiff, styleList[styleIndex]);
                Console.WriteLine("Bots[" + index + "] : " + Bots[index].Name + " : " + Bots[index].Difficult + " : " + Bots[index].Style.ToString());
            }
            Selected = null;
            IsSelected = false;
        }

        private List<GameStyle> styleList = new List<GameStyle>(){ GameStyle.BRAVE, GameStyle.COWARDLY, GameStyle.RANDOM, GameStyle.STRATEGIC };
        public List<string> StyleList { get; set; }


        public SinglePlayerGameManager GetGameManager()
        {
            SinglePlayerGameConfig config = new SinglePlayerGameConfig()
            {
                MineNumber = this.RoundNumber,
                botList = new List<AIPlayer>(bots).ToArray(),
                DecisionTime = this.DecisionTime,
                DragonsMinimalDeep = this.DragonsDeep,
                PlayerNickname = "Adventurer",
            };

            return new SinglePlayerGameManager(config);
        }

        public double minDragonsDeep = -1;
        public double maxDragonsDeep = 30;
        public double minRoundNumber = 1;
        public double maxRoundNumber = 5;
        public double minDecisionTime = 10;
        public double maxDecisiontime = 60;
        private int minPlayerNumber = 1;
        private int maxPlayernumber = 7;
        public double maxDifficulty = 10;
        public double minDifficulty = 1;

        private void SetDefault()
        {
            this.DecisionTime = 15;
            this.DragonsDeep = 5;
            this.RoundNumber = 3;
            this.bots = new ObservableCollection<AIPlayer>();
            AddAIPlayer();
            AddAIPlayer();
            AddAIPlayer();

            this.Selected = bots[0];
            this.isSelected = false;

            StyleList = new List<string>();
            styleList.ForEach(style => StyleList.Add(style.ToString()));
        }
    }
}
