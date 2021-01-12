using EmeraldRush.Model.AIMode.AILogic;
using EmeraldRush.Model.AIMode.Player;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EmeraldRush.Model.AIMode.Game
{
    class SinglePlayerGameInstance
    {
        private AIPlayer[] bots;
        private SinglePlayerGameConfig gameConfig;
        private PlayerDecision[] playersDecisions;
        private bool[] isExploring;
        private List<int> cardRemoved;
        public GameInstance PublicGameData;
        private CardDeck cardDeck;
        private Action<GameInstance> updateUI;
        private bool isGameAborted;
        private int decisionTimeTolerance;

        public SinglePlayerGameInstance(SinglePlayerGameConfig config)
        {
            cardDeck = new CardDeck();
            gameConfig = config;
            bots = config.BotList;
            cardRemoved = new List<int>();
            decisionTimeTolerance = 2;
            List<PlayersPublic> playersPublic = new List<PlayersPublic>();


            foreach (AIPlayer bot in bots)
            {
                playersPublic.Add(new PlayersPublic(playersPublic.Count, bot.Name, 0, 0, "bot" + playersPublic.Count + "_" + bot.Name));

                switch (bot.Style)
                {
                    case GameStyle.BRAVE: bot.DecisionStrategy = new BraveDecisionStrategy(bot.Difficult); break;
                    case GameStyle.RANDOM: bot.DecisionStrategy = new RandomDecisionStrategy(bot.Difficult); break;
                    default: bot.DecisionStrategy = new BraveDecisionStrategy(bot.Difficult); break;
                }
            }

            playersPublic.Add(new PlayersPublic(playersPublic.Count, config.PlayerNickname, 0, 0, "p0_" + config.PlayerNickname));

            isExploring = new bool[playersPublic.Count];
            playersDecisions = new PlayerDecision[playersPublic.Count];

            PublicGameData = new GameInstance()
            {
                CurrentMineID = 0,
                DecisionTime = gameConfig.DecisionTime,
                DragonMinimalDeep = gameConfig.DragonsMinimalDeep,
                MineNumber = gameConfig.MineNumber,
                Mines = new GameModel.Mine[gameConfig.MineNumber],
                RoundCooldownTime = 10,
                RoundID = 0,
                PlayersPublic = playersPublic.ToArray(),
                PublicState = GameStatus.WAITING_FOR_FIRST,
                GameUID = "SinglePlayerGame"
            };

            for (int iter = 0; iter < PublicGameData.MineNumber; iter++)
            {
                PublicGameData.Mines[iter] = new Mine();
            }


        }

        public string GetUserUID()
        {
            return PublicGameData.PlayersPublic[PublicGameData.PlayersPublic.Length - 1].uid;
        }

        public GameInstance Next()
        {
            LogManager.Print("gs(" + PublicGameData.RoundID + "): " + PublicGameData.PublicState.ToString(), "SinglePlayerGameInstance");

            if (PublicGameData.PublicState == GameStatus.WAITING_FOR_FIRST )
            {
                PlayFirstCard();
            }
            else
            {
                PlayNextCard();
            }

            LogManager.Print("gs(" + PublicGameData.RoundID + "): " + PublicGameData.PublicState.ToString(), "SinglePlayerGameInstance");
            updateUI(PublicGameData);

            if (PublicGameData.PublicState == GameStatus.WAITING_FOR_MOVE)
            {
                LogManager.Print("waiting for move...", "SinglePlayerGameInstance");
                ScheduleNextMove(PublicGameData.DecisionTime + decisionTimeTolerance);

            }
            else if (PublicGameData.PublicState == GameStatus.ROUND_SUMMARY)
            {
                LogManager.Print("time for round summary", "SinglePlayerGameInstance");
                PublicGameData.PublicState = GameStatus.WAITING_FOR_FIRST;


                ScheduleNextMove(PublicGameData.RoundCooldownTime + decisionTimeTolerance);
            }

            return PublicGameData;
        }

        private void PlayNextCard()
        {

            List<int> goingPlayer = new List<int>();
            List<int> leavingPlayer = new List<int>();

            for (int iter = 0; iter < playersDecisions.Length; iter++)
            {
                if (playersDecisions[iter] == PlayerDecision.GO_FURTHER && isExploring[iter])
                {
                    goingPlayer.Add(iter);

                }
                else if (isExploring[iter])
                {
                    leavingPlayer.Add(iter);
                }
            }

            if (goingPlayer.Count == 0)
            {

                for (int iter = 0; iter < playersDecisions.Length; iter++)
                {
                    playersDecisions[iter] = PlayerDecision.GO_FURTHER;
                    isExploring[iter] = true;
                }

                foreach (int playerId in leavingPlayer)
                {
                    if (leavingPlayer.Count == 1)
                    {
                        PublicGameData.PlayersPublic[playerId].pocket += PublicGameData.GetCurrent().EmeraldsForTake;
                        PublicGameData.GetCurrent().EmeraldsForTake = 0;
                    }

                    PublicGameData.PlayersPublic[playerId].chest += PublicGameData.PlayersPublic[playerId].pocket;
                    PublicGameData.PlayersPublic[playerId].status = PlayerStatus.RESTING;
                }

                if (PublicGameData.MineNumber > PublicGameData.CurrentMineID + 1)
                {
                    PublicGameData.GetCurrent().MineState = MineStatus.VISITED;
                    PublicGameData.CurrentMineID++;
                    PublicGameData.GetCurrent().MineState = MineStatus.CURRENT;
                    PublicGameData.PublicState = GameStatus.ROUND_SUMMARY;
                }
                else
                {
                    PublicGameData.GetCurrent().MineState = MineStatus.VISITED;
                    PublicGameData.PublicState = GameStatus.FINISHED;
                }


                LogManager.Print("Round finished - everybody leave cave", "SinglePlayerGameInstance");
                return;
            }
            else
            {

                foreach (int playerId in leavingPlayer)
                {
                    if (leavingPlayer.Count == 1)
                    {
                        PublicGameData.PlayersPublic[playerId].pocket += PublicGameData.GetCurrent().EmeraldsForTake;
                        PublicGameData.GetCurrent().EmeraldsForTake = 0;
                    }

                    PublicGameData.PlayersPublic[playerId].chest += PublicGameData.PlayersPublic[playerId].pocket;
                    PublicGameData.PlayersPublic[playerId].status = PlayerStatus.RESTING;
                    isExploring[playerId] = false;
                }


                //selecting card
                Card selectedCard = cardDeck.GetRandomCard(cardRemoved, PublicGameData.GetCurrent().Node, DragonPossible());

                PublicGameData.GetCurrent().Node.Add(selectedCard.CardID);

                //If emeralds selected
                if (selectedCard.Type == CardType.ARTIFACT || selectedCard.Type == CardType.EMERALDS)
                {
                    int rest = selectedCard.EmeraldValue % goingPlayer.Count;
                    int byPlayer = (selectedCard.EmeraldValue - rest) / goingPlayer.Count;

                    if (rest + PublicGameData.GetCurrent().EmeraldsForTake > goingPlayer.Count)
                    {
                        int forF = rest + PublicGameData.GetCurrent().EmeraldsForTake;
                        rest = forF % goingPlayer.Count;
                        byPlayer += (forF - rest) / goingPlayer.Count;
                    }

                    foreach (int playerId in goingPlayer)
                    {
                        this.PublicGameData.PlayersPublic[playerId].pocket += byPlayer;
                        playersDecisions[playerId] = PlayerDecision.UNKNOWN;
                    }

                    PublicGameData.GetCurrent().EmeraldsForTake = rest;

                    PublicGameData.PublicState = GameStatus.WAITING_FOR_MOVE;
                    PublicGameData.RoundID++;
                    return;
                }
                else
                {
                    List<int> resoult = CheckIfRoundIsFinnished(PublicGameData.GetCurrent().Node, selectedCard.Type);

                    if (resoult.Count > 0 || selectedCard.Type == CardType.DRAGON)
                    {

                        if (resoult.Count > 0)
                            cardRemoved.AddRange(resoult);
                        else
                            cardRemoved.Add(selectedCard.CardID);

                        for (int iter = 0; iter < playersDecisions.Length; iter++)
                        {
                            playersDecisions[iter] = PlayerDecision.GO_FURTHER;
                            isExploring[iter] = true;

                        }

                        goingPlayer.ForEach(playerId =>
                        {
                            PublicGameData.PlayersPublic[playerId].status = PlayerStatus.DEAD;
                            PublicGameData.PlayersPublic[playerId].pocket = 0;
                        });


                        if (PublicGameData.MineNumber > PublicGameData.CurrentMineID + 1)
                        {
                            PublicGameData.GetCurrent().MineState = MineStatus.VISITED;
                            PublicGameData.CurrentMineID++;
                            PublicGameData.GetCurrent().MineState = MineStatus.CURRENT;
                            PublicGameData.PublicState = GameStatus.ROUND_SUMMARY;
                        }
                        else
                        {
                            PublicGameData.GetCurrent().MineState = MineStatus.VISITED;
                            PublicGameData.PublicState = GameStatus.FINISHED;
                        }


                        LogManager.Print("Round finished by: " + selectedCard.Type.ToString(), "SinglePlayerGameInstance");
                        return;


                    }
                    else
                    {

                        for (int iter = 0; iter < 0; iter++)
                        {
                            playersDecisions[iter] = PlayerDecision.UNKNOWN;
                        }

                        PublicGameData.PublicState = GameStatus.WAITING_FOR_MOVE;
                        PublicGameData.RoundID++;
                        return;
                    }

                }

            }

        }

        private bool DragonPossible()
        {
            if (gameConfig.DragonsMinimalDeep < 0)
            {
                return false;
            }
            else
            {
                return (PublicGameData.GetCurrent().Node.Count >= gameConfig.DragonsMinimalDeep);
            }
        }

        private void PlayFirstCard()
        {
            Card selectedCard = cardDeck.GetRandomCard(cardRemoved, PublicGameData.GetCurrent().Node, DragonPossible());

            foreach (PlayersPublic player in PublicGameData.PlayersPublic)
            {
                player.status = PlayerStatus.EXPLORING;
                player.pocket = 0;
            }

            if (selectedCard.Type == CardType.ARTIFACT || selectedCard.Type == CardType.EMERALDS)
            {
                int rest = selectedCard.EmeraldValue % PublicGameData.PlayersPublic.Length;
                int byPlayer = (selectedCard.EmeraldValue - rest) / PublicGameData.PlayersPublic.Length;

                if (rest + PublicGameData.GetCurrent().EmeraldsForTake > PublicGameData.PlayersPublic.Length)
                {
                    int forF = rest + PublicGameData.GetCurrent().EmeraldsForTake;
                    rest = forF % PublicGameData.PlayersPublic.Length;
                    byPlayer += (forF - rest) / PublicGameData.PlayersPublic.Length;
                }

                foreach (PlayersPublic player in PublicGameData.PlayersPublic)
                {
                    player.pocket += byPlayer;
                }

                PublicGameData.GetCurrent().EmeraldsForTake += rest;
            }

            PublicGameData.GetCurrent().Node.Add(selectedCard.CardID);

            for (int iter = 0; iter < playersDecisions.Length; iter++)
            {
                playersDecisions[iter] = PlayerDecision.UNKNOWN;
                isExploring[iter] = true;
            }

            PublicGameData.PublicState = GameStatus.WAITING_FOR_MOVE;
            PublicGameData.RoundID++;
        }

        private void LetBotsDecide(GameInstance game)
        {

            for (int iter = 0; iter < bots.Length; iter++)
            {
                if (game.PlayersPublic[iter].status == PlayerStatus.EXPLORING) playersDecisions[iter] = bots[iter].DecisionStrategy.MakeDecision(game, iter);
            }

        }

        public void MakeDecision(bool decision)
        {
            LogManager.Print("Decision made: " + decision, "SinglePlayerGameInstance");
            playersDecisions[PublicGameData.PlayersPublic.Length - 1] = (decision ? PlayerDecision.GO_FURTHER : PlayerDecision.GO_BACK);
        }

        public void Start(Action<GameInstance> callback)
        {
            updateUI = callback;
            isGameAborted = false;
        }

        public void StopGame()
        {
            isGameAborted = true;
            PublicGameData.PublicState = GameStatus.FINISHED;
        }

        public void ScheduleNextMove(int seconds)
        {
            LogManager.Print("waiting", "SinglePlayerGameInstance");
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                if (!isGameAborted)
                {
                    LogManager.Print("doing move;", "SinglePlayerGameInstance");
                    if (PublicGameData.PublicState == GameStatus.WAITING_FOR_MOVE)
                        LetBotsDecide(PublicGameData);

                    Next();
                }

                return false;
            });
        }


        private List<int> CheckIfRoundIsFinnished(List<int> usedCard, CardType selectedType)
        {
            List<int> cardtoRemove = new List<int>();

            cardtoRemove = usedCard.FindAll(cardID => cardDeck.GetCard(cardID).Type == selectedType);

            if (cardtoRemove.Count == 3)
            {
                return cardtoRemove;
            }
            else
            {
                return new List<int>();
            }

        }


    }
}
