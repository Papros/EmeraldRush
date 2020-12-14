using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.ConfigEnum;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameManager;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using EmeraldRush.Services.FirebaseAuthService;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
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
        public bool waitingForDecision;
        public bool makingDecision;
        private Action<int> ScrollToNewCard;
        private Action<int> AskForDecision;

        private IGameManager GameManager;

        public MineExploringViewModel(Action<int> ScrollToNewCard, Action<int> AskForDecision, IGameManager manager)
        {
            this.GameManager = manager;
            this.playerUID = manager.GetUserUID();
            this.ScrollToNewCard = ScrollToNewCard;
            this.AskForDecision = AskForDecision;

            this.Adventurers = new Player[0];
            this.Nodes = new Card[0];

            this.Pocket = 0;
            this.Chest = 0;
            this.MineIndex = "0 / 0";
            this.PathLength = 0;

            InitializeObjects();
            

        }

        private void InitializeObjects()
        {
            LogManager.Print("Game initalizing", "MineExploringVM");
            GameManager.Subscribe(UpdateData);           
        }

        private void UpdateData(GameInstance gameInstance)
        {
            if (gameInstance != null)
            {
                CardDeck deck = new CardDeck();

                this.GameUID = gameInstance.GameUID;

                if (gameInstance.PlayersPublic != null)
                {
                    var adventurers = new Player[gameInstance.PlayersPublic.Length];
                    for(int iter = 0; iter < Adventurers.Length; iter++)
                    {
                        adventurers[iter] = new Player(gameInstance.PlayersPublic[iter]);
                    }
                    this.Adventurers = adventurers;
                }

                if (gameInstance.GetCurrent() != null)
                {
                    this.Nodes = (new Card[]{new Card(-1,CardType.ENTRY)}).Concat(deck.GetThisDeck(gameInstance.GetCurrent().Node.ToArray())).ToArray();
                }

                this.MineIndex = (gameInstance.CurrentMineID + 1).ToString() + " / " + gameInstance.MineNumber.ToString();
                this.PathLength = nodes.Length;

                if (gameInstance.GetCurrent() != null)
                {
                    this.EmeraldsForTake = gameInstance.GetCurrent().EmeraldsForTake;
                }

                var player = gameInstance.GetPlayerData(this.playerUID);

                if (player != null)
                {
                    this.Pocket = player.pocket;
                    this.Chest = player.chest;
                    this.playerID = player.id;
                    this.makingDecision = (player.status == PlayerStatus.EXPLORING && gameInstance.PublicState == GameStatus.WAITING_FOR_MOVE);
                }

                ScrollToNewCard.Invoke(Nodes.Length - 1);
                if (makingDecision)
                {
                    waitingForDecision = true;
                    AskForDecision.Invoke(gameInstance.DecisionTime);
                }
                else
                {
                    waitingForDecision = false;
                }

            }

        }

        public void MakeDecision(bool decision)
        {
            waitingForDecision = false;

            GameManager.MakeDecision(decision, this.playerID, this.GameUID);

        }

    }
}
