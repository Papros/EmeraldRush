using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using EmeraldRush.Services.FirebaseAuthService;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Collections.Generic;
using System.Text;
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

        private int mineIndex;

        public int MineIndex
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

        private string plyerUID;

        public MineExploringViewModel()
        {
            this.plyerUID = FirebaseAuthManager.GetUserUID();

            this.Adventurers = new PlayersPublic[0];
            this.Nodes = new Card[0];

            this.Pocket = 0;
            this.Chest = 0;
            this.MineIndex = 0;
            this.PathLength = 0;

            if(!test())
            InitializeObjects();
        }

        private async void InitializeObjects()
        {
            Console.WriteLine("Initializing...");
           // GameInstance gameInstance = await FirebaseGameManager.GetInstance().GetGame();
           // UpdateData(gameInstance);

            Console.WriteLine("Initialized.");

            MessagingCenter.Subscribe<FirebaseGameManager,GameInstance>(this, AplicationConstants.GAME_UPDATE_MSG, (callback, data) =>
            {
                if(data != null)
                 this.UpdateData(data);
            });
        }

        private void UpdateData(GameInstance gameInstance)
        {
            if (gameInstance != null)
            {
                Console.WriteLine("Ubdating and game instance not null");
                CardDeck deck = new CardDeck();

                if(gameInstance.PlayersPublic != null)
                {
                    this.Adventurers = gameInstance.PlayersPublic;
                }
                else { Console.WriteLine("Adventures = null"); }

                Console.WriteLine("Part 1");

                if (gameInstance.GetCurrent() != null)
                {
                    this.Nodes = deck.GetThisDeck(gameInstance.GetCurrent().Node);
                }
                else { Console.WriteLine("currentMine = null");}

                Console.WriteLine("Part 2");


                this.MineIndex = gameInstance.CurrentMineID;
                this.PathLength = nodes.Length;

                Console.WriteLine("Part 3");

                if (gameInstance.GetCurrent() != null)
                { 
                    this.EmeraldsForTake = gameInstance.GetCurrent().EmeraldsForTake;
                }
                else { Console.WriteLine("currentMine = null"); }

                Console.WriteLine("Part 4");

                if (gameInstance.GetPlayerData(this.plyerUID) != null)
                {
                    this.pocket = gameInstance.GetPlayerData(this.plyerUID).pocket;
                    this.chest = gameInstance.GetPlayerData(this.plyerUID).chest;
                }
                else { Console.WriteLine("Player = null"); }

                Console.WriteLine("Finished");

            }
            
        }

        private bool test()
        {
            if(FirebaseGameManager.GetInstance() == null)
            {
                CardDeck deck = new CardDeck();
                int[] nod = { 0, 3, 6, 9, 12, 15, 18, 25 };
                this.Nodes = deck.GetThisDeck(nod);
                this.MineIndex = 2;
                this.PathLength = nodes.Length;
                this.EmeraldsForTake = 15;
                this.Pocket = 12;
                this.Chest = 25;
                return true;
            }

            return false;
        }

    }
}
