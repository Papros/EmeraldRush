﻿using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using EmeraldRush.Services.FirebaseAuthService;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Collections.Generic;
using System.Text;
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
        private Action<int> ScrollToNewCard;
        private Action<int> AskForDecision;

        public MineExploringViewModel(Action<int> ScrollToNewCard, Action<int> AskForDecision)
        {
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

            MessagingCenter.Subscribe<FirebaseGameManager, GameInstance>(this, AplicationConstants.GAME_UPDATE_MSG, (callback, data) =>
            {
                if (data != null)
                {
                    this.UpdateData(data);
                    ScrollToNewCard.Invoke(Nodes.Length - 1);
                    waitingForDecision = true;
                    //  AskForDecision.Invoke(data.DecisionTime);   
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
                    this.Nodes = deck.GetThisDeck(gameInstance.GetCurrent().Node);
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
                }


            }

        }

        public void MakeDecision(bool decision)
        {
            waitingForDecision = false;

            if (decision)
            {
                Task.Run(() => DecisionManager.SendDecision(this.GameUID, this.playerID, PlayerDecision.GO_BACK));
            }
            else
            {
                Task.Run(() => DecisionManager.SendDecision(this.GameUID, this.playerID, PlayerDecision.GO_FURTHER));
            }

        }

    }
}
