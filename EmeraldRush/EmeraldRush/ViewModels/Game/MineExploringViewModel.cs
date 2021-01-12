using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameManager;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Player = EmeraldRush.Model.GameModel.Player;

namespace EmeraldRush.ViewModels.Game
{
    class MineExploringViewModel : BaseViewModel
    {
        private ObservableCollection<Card> nodes;
        public ObservableCollection<Card> Nodes
        {
            get { return nodes; }
            private set { SetProperty(ref nodes, value, "Nodes"); }
        }

        private string mineIndex;

        public string MineIndex
        {
            get { return mineIndex; }
            private set { SetProperty(ref mineIndex, value, "MineIndex"); }
        }

        private Player[] adventurers;
        public Player[] Adventurers
        {
            get { return adventurers; }
            private set { SetProperty(ref adventurers, value, "Adventurers"); }
        }

        private int emeraldsForTake;
        public int EmeraldsForTake
        {
            get { return emeraldsForTake; }
            private set { SetProperty(ref emeraldsForTake, value, "EmeraldsForTake"); }
        }

        public int pathLength;
        public int PathLength
        {
            get { return pathLength; }
            private set { SetProperty(ref pathLength, value, "PathLength"); }
        }

        private int pocket;
        public int Pocket
        {
            get { return pocket; }
            private set { SetProperty(ref pocket, value, "Pocket" ); }
        }

        private int chest;
        public int Chest
        {
            get { return chest; }
            private set { SetProperty(ref chest, value, "Chest"); }
        }

        private bool roundSummaryBoxVisible;
        public bool RoundSummaryBoxVisible
        {
            get { return roundSummaryBoxVisible; }
            set { SetProperty(ref roundSummaryBoxVisible, value, "RoundSummaryBoxVisible");  }
        }

        private bool isGameEnded;
        public bool IsGameEnded
        {
            get { return isGameEnded; }
            set { SetProperty(ref isGameEnded, value, "IsGameEnded"); }
        }


        private string summaryImagePath;
        public string SummaryImagePath
        {
            get { return summaryImagePath; }
            set { SetProperty(ref summaryImagePath, value, "SummaryImagePath"); }
        }

        private string summaryLabel;
        public string SummaryLabel
        {
            get { return summaryLabel; }
            set { SetProperty(ref summaryLabel, value, "SummaryLabel"); }
        }

        private string playerUID;
        private int playerID;
        private string GameUID;
        public bool WaitingForDecision;
        private bool makingDecision;
        private Action<int> scrollToNewCard;
        private Action<int> askForDecision;
        private IGameManager gameManager;
        private CardDeck deck;

        public bool DecisionBoxDisapearing;

        public MineExploringViewModel(Action<int> ScrollToNewCard, Action<int> AskForDecision, IGameManager manager)
        {
            gameManager = manager;
            playerUID = manager.GetUserUID();
            this.scrollToNewCard = ScrollToNewCard;
            this.askForDecision = AskForDecision;
            this.RoundSummaryBoxVisible = false;
            this.SummaryImagePath = "cave.png";
            deck = new CardDeck();
            Adventurers = new Player[0];
            Nodes = new ObservableCollection<Card>();
            SummaryLabel = "Waiting for next round...";
            IsGameEnded = false;
            DecisionBoxDisapearing = new Model.SettingsManager.SettingManager().GetValue(Model.SettingsManager.SettingsKey.DECISION_BOX, false);
            Pocket = 0;
            Chest = 0;
            MineIndex = "0 / 0";
            PathLength = 0;
            InitializeObjects();

        }

        private void InitializeObjects()
        {
            LogManager.Print("Game initalizing", "MineExploringVM");
            gameManager.Subscribe(UpdateData);
        }

        private void UpdateData(GameInstance gameInstance)
        {
            if (gameInstance != null)
            {
                GameUID = gameInstance.GameUID;

                if (gameInstance.PublicState == GameStatus.FINISHED)
                {
                    if (gameInstance.PlayersPublic.All((player) => player.status == PlayerStatus.RESTING))
                    {
                        SummaryImagePath = "camp.png";
                    }
                    else
                    { 
                        SummaryImagePath = deck.GetCard(gameInstance.GetCurrent().GetLastCardID()).ImagePath;
                    };
                    SummaryLabel = "Click to back to menu";
                    IsGameEnded = true;
                    RoundSummaryBoxVisible = true;
                }
                else if (gameInstance.PublicState == GameStatus.ROUND_SUMMARY)
                {

                    if (gameInstance.PlayersPublic.All((player) => player.status == PlayerStatus.RESTING))
                    {
                        SummaryImagePath = "camp.png";
                    }
                    else
                    {
                        SummaryImagePath = deck.GetCard(gameInstance.GetCurrent().GetLastCardID()).ImagePath;
                    };

                    SummaryLabel = "Next round in about " + gameInstance.RoundCooldownTime + " seconds";
                    IsGameEnded = false;
                    RoundSummaryBoxVisible = true;
                }
                else
                {
                    RoundSummaryBoxVisible = false;
                    IsGameEnded = false;
                }


                if (gameInstance.PlayersPublic != null)
                {
                    var adventurers = new Player[gameInstance.PlayersPublic.Length];
                    for (int iter = 0; iter < Adventurers.Length; iter++)
                    {
                        adventurers[iter] = new Player(gameInstance.PlayersPublic[iter]);
                    }
                    if(RoundSummaryBoxVisible) Array.Sort(adventurers);
                    Adventurers = adventurers;
                }

                if (gameInstance.GetCurrent() != null)
                { 
                    Nodes = new ObservableCollection<Card>((new Card[] { new Card(-1, CardType.ENTRY) }).Concat(deck.GetThisDeck(gameInstance.GetCurrent().Node.ToArray())) );
                }

                MineIndex = (gameInstance.CurrentMineID + 1).ToString() + " / " + gameInstance.MineNumber.ToString();
                PathLength = nodes.Count;

                if (gameInstance.GetCurrent() != null)
                {
                    EmeraldsForTake = gameInstance.GetCurrent().EmeraldsForTake;
                }

                var player = gameInstance.GetPlayerData(playerUID);

                if (player != null)
                {
                    Pocket = player.pocket;
                    Chest = player.chest;
                    playerID = player.id;
                    makingDecision = (player.status == PlayerStatus.EXPLORING && gameInstance.PublicState == GameStatus.WAITING_FOR_MOVE);
                }

                scrollToNewCard.Invoke(Nodes.Count - 1);
                if (makingDecision)
                {
                    WaitingForDecision = true;
                    askForDecision.Invoke(gameInstance.DecisionTime);
                }
                else
                {
                    WaitingForDecision = false;
                }

            }

        }

        public void MakeDecision(bool decision)
        {
            WaitingForDecision = false;

            gameManager.MakeDecision(decision, playerID, GameUID);

        }

    }
}
