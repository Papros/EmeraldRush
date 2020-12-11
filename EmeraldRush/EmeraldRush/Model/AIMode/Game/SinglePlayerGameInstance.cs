using EmeraldRush.Model.AIMode.Player;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.AIMode.Game
{
    class SinglePlayerGameInstance
    {
        private AIPlayer[] botPlayer;
        private SinglePlayerGameConfig gameConfig;
        private int[] RemovedCard;
        private GameInstance GameInstance;
        private GameStatus status;
        private CardDeck deck;

        public SinglePlayerGameInstance(SinglePlayerGameConfig config, AIPlayer[] botList, string PlayerNickname)
        {
            this.gameConfig = config;
            this.botPlayer = botList;

            List<PlayersPublic> playersPublic = new List<PlayersPublic>();
            
            foreach( AIPlayer player in botList)
            {
                playersPublic.Add(new PlayersPublic(playersPublic.Count,player.Name));
            }

            playersPublic.Add(new PlayersPublic(playersPublic.Count, PlayerNickname));

            GameInstance = new GameInstance();
            GameInstance = new GameInstance()
            {
                CurrentMineID = 0,
                DecisionTime = gameConfig.DecisionTime,
                DragonMinimalDeep = gameConfig.DragonsMinimalDeep,
                MineNumber = gameConfig.MineNumber,
                Mines = new GameModel.Mine[gameConfig.MineNumber],
                RoundCooldownTime = 10,
                RoundID = 0,
                PlayersPublic = playersPublic.ToArray(),
                PublicState = 0,
                GameUID = "SinglePlayerGame"
            };

        }

        public GameInstance Next()
        {
            if(this.status == GameStatus.WAITING_FOR_FIRST)
            {
                PlayFirstCard();
            }
            else
            {
                PlayNextCard();
            }

            return this.GameInstance;
        }

        private void PlayNextCard()
        {
            
        }

        private void PlayFirstCard()
        {

        }

        private void LetBotsDecide() { 
        }

        public void MakeDecision(PlayerDecision decision)
        {

        }

    }
}
