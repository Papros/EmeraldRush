using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameManager;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using System;
using System.Linq;
using Player = EmeraldRush.Model.GameModel.Player;

namespace EmeraldRush.ViewModels.Game
{
    class MineExploringViewModel : BaseViewModel
    {
        private Card[] nodes;
        public Card[] Nodes
        {
            get { return nodes; }
            private set { SetProperty(ref nodes, value); }
        }

        private string mineIndex;

        public string MineIndex
        {
            get { return mineIndex; }
            private set { SetProperty(ref mineIndex, value); }
        }

        private Player[] adventurers;
        public Player[] Adventurers
        {
            get { return adventurers; }
            private set { SetProperty(ref adventurers, value); }
        }

        private int emeraldsForTake;
        public int EmeraldsForTake
        {
            get { return emeraldsForTake; }
            private set { SetProperty(ref emeraldsForTake, value); }
        }

        public int pathLength;
        public int PathLength
        {
            get { return pathLength; }
            private set { SetProperty(ref pathLength, value); }
        }

        private int pocket;
        public int Pocket
        {
            get { return pocket; }
            private set { SetProperty(ref pocket, value); }
        }

        private int chest;
        public int Chest
        {
            get { return chest; }
            private set { SetProperty(ref chest, value); }
        }

        private string playerUID;
        private int playerID;
        private string GameUID;
        public bool WaitingForDecision;
        private bool makingDecision;
        private Action<int> scrollToNewCard;
        private Action<int> askForDecision;
        private IGameManager gameManager;

        public MineExploringViewModel(Action<int> ScrollToNewCard, Action<int> AskForDecision, IGameManager manager)
        {
            gameManager = manager;
            playerUID = manager.GetUserUID();
            this.scrollToNewCard = ScrollToNewCard;
            this.askForDecision = AskForDecision;

            Adventurers = new Player[0];
            Nodes = new Card[0];

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
                CardDeck deck = new CardDeck();

                GameUID = gameInstance.GameUID;

                if (gameInstance.PlayersPublic != null)
                {
                    var adventurers = new Player[gameInstance.PlayersPublic.Length];
                    for (int iter = 0; iter < Adventurers.Length; iter++)
                    {
                        adventurers[iter] = new Player(gameInstance.PlayersPublic[iter]);
                    }
                    Adventurers = adventurers;
                }

                if (gameInstance.GetCurrent() != null)
                {
                    Nodes = (new Card[] { new Card(-1, CardType.ENTRY) }).Concat(deck.GetThisDeck(gameInstance.GetCurrent().Node.ToArray())).ToArray();
                }

                MineIndex = (gameInstance.CurrentMineID + 1).ToString() + " / " + gameInstance.MineNumber.ToString();
                PathLength = nodes.Length;

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

                scrollToNewCard.Invoke(Nodes.Length - 1);
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
