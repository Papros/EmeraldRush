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

        private PlayersPublic[] adventurers;
        public PlayersPublic[] Adventurers
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
            this.playerUID = FirebaseAuthManager.GetUserUID();
            this.ScrollToNewCard = ScrollToNewCard;
            this.AskForDecision = AskForDecision;

            this.Adventurers = new PlayersPublic[0];
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
            Task.Run(async () =>
            {

                var data = await FirebaseGameManager.GetInstance().GetGame();
                LogManager.Print("Game force update", "MineExploringVM");
                if (data != null)
                {
                    this.UpdateData(data);
                    ScrollToNewCard.Invoke(Nodes.Length - 1);
                    if (makingDecision)
                    {
                        waitingForDecision = true;
                        AskForDecision.Invoke(data.DecisionTime);
                    }
                    LogManager.Print("Game view updated.", "MineExploringVM");

                }
                else
                {
                    LogManager.Print("Null gameInstance", "MineExploringVM");
                }

            });


            MessagingCenter.Subscribe<FirebaseGameManager, GameInstance>(this, AplicationConstants.GAME_UPDATE_MSG, (callback, data) =>
            {
                if (data != null)
                {
                    this.UpdateData(data);
                    ScrollToNewCard.Invoke(Nodes.Length - 1);
                    if (makingDecision)
                    {
                        waitingForDecision = true;
                        AskForDecision.Invoke(data.DecisionTime);
                    }
                    else
                    {
                        waitingForDecision = false;
                    }
                    LogManager.Print("Game view updated.", "MineExploringVM");

                }
                else
                {
                    LogManager.Print("Null gameInstance", "MineExploringVM");
                }

            });
        }

        private void UpdateData(GameInstance gameInstance)
        {
            if (gameInstance != null)
            {
                CardDeck deck = new CardDeck();

                this.GameUID = gameInstance.GameUID;

                if (gameInstance.PlayersPublic != null)
                {
                    this.Adventurers = gameInstance.PlayersPublic;
                }

                if (gameInstance.GetCurrent() != null)
                {
                    this.Nodes = (new Card[]{new Card(-1,CardType.ENTRY)}).Concat(deck.GetThisDeck(gameInstance.GetCurrent().Node)).ToArray();
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
                    this.makingDecision = (player.status == PlayerStatus.EXPLORING );
                }


            }

        }

        public void MakeDecision(bool decision)
        {
            waitingForDecision = false;

            if (decision)
            {
                Task.Run(() => DecisionManager.SendDecision(this.GameUID, this.playerID, PlayerDecision.GO_FURTHER));
            }
            else
            {
                Task.Run(() => DecisionManager.SendDecision(this.GameUID, this.playerID, PlayerDecision.GO_BACK));
            }

        }

    }
}
