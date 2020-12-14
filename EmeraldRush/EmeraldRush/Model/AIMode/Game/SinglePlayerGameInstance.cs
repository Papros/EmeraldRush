using EmeraldRush.Model.AIMode.AILogic;
using EmeraldRush.Model.AIMode.Player;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Model.GameModel;
using EmeraldRush.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.Model.AIMode.Game
{
    class SinglePlayerGameInstance
    {
        private AIPlayer[] botPlayer;
        private SinglePlayerGameConfig gameConfig;
        private PlayerDecision[] playersDecisions;
        private bool[] isExploring;
        private List<int> RemovedCard;
        public GameInstance GameInstance;
        private CardDeck deck;
        private Action<GameInstance> UIupdate;
        private bool abortGame;
        private int timeTolerance;

        public SinglePlayerGameInstance(SinglePlayerGameConfig config)
        {
            this.deck = new CardDeck();
            this.gameConfig = config;
            this.botPlayer = config.botList;
            this.RemovedCard = new List<int>();
            this.timeTolerance = 2;
            List<PlayersPublic> playersPublic = new List<PlayersPublic>();
            

            foreach(AIPlayer bot in botPlayer)
            {
                playersPublic.Add(new PlayersPublic(playersPublic.Count, bot.Name, 0, 0, "bot" + playersPublic.Count + "_" + bot.Name));

                switch (bot.Style)
                {
                    case GameStyle.BRAVE: bot.DecisionStrategy = new BraveDecisionStrategy(bot.Difficult); break;
                    case GameStyle.RANDOM: bot.DecisionStrategy = new RandomDecisionStrategy(bot.Difficult); break;
                    default: bot.DecisionStrategy = new BraveDecisionStrategy(bot.Difficult); break;
                }
            }

            playersPublic.Add(new PlayersPublic(playersPublic.Count, config.PlayerNickname, 0,0, "p0_"+config.PlayerNickname));

            isExploring = new bool[playersPublic.Count];
            playersDecisions = new PlayerDecision[playersPublic.Count];

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
                PublicState = GameStatus.WAITING_FOR_FIRST,
                GameUID = "SinglePlayerGame"
            };

            for(int iter=0; iter < GameInstance.MineNumber; iter++)
            {
                GameInstance.Mines[iter] = new Mine();
            }


        }

        public string GetUserUID()
        {
            return GameInstance.PlayersPublic[GameInstance.PlayersPublic.Length-1].uid;
        }

        public GameInstance Next()
        {
            LogManager.Print("gs("+GameInstance.RoundID+"): " + GameInstance.PublicState.ToString(), "SinglePlayerGameInstance");

            if (GameInstance.PublicState == GameStatus.WAITING_FOR_FIRST)
            {
                PlayFirstCard();
            }
            else
            {
                PlayNextCard();
            }

            LogManager.Print("gs(" + GameInstance.RoundID + "): " + GameInstance.PublicState.ToString(), "SinglePlayerGameInstance");
            UIupdate(this.GameInstance);

            if (GameInstance.PublicState == GameStatus.WAITING_FOR_MOVE )
            {
                LogManager.Print("waiting for move...", "SinglePlayerGameInstance");
                ScheduleNextMove(GameInstance.DecisionTime+timeTolerance);

            }else if(GameInstance.PublicState == GameStatus.ROUND_SUMMARY)
            {
                LogManager.Print("time for round summary", "SinglePlayerGameInstance");
                ScheduleNextMove(GameInstance.RoundCooldownTime + timeTolerance);
            }

            return this.GameInstance;
        }

        private void PlayNextCard()
        {
            
            List<int> goingPlayer = new List<int>();
            List<int> leavingPlayer = new List<int>();
        
            for(int iter=0; iter < playersDecisions.Length; iter++)
            {
                if (playersDecisions[iter] == PlayerDecision.GO_FURTHER && isExploring[iter])
                {
                    goingPlayer.Add(iter);

                }else if(isExploring[iter])
                {
                    leavingPlayer.Add(iter);
                }
            }
        
            if(goingPlayer.Count == 0)
            {

                for(int iter=0; iter < playersDecisions.Length; iter++)
                {
                    playersDecisions[iter] = PlayerDecision.GO_FURTHER;
                    isExploring[iter] = true;
                }

                foreach(int playerId in leavingPlayer)
                {
                    if(leavingPlayer.Count == 1)
                    {
                        GameInstance.PlayersPublic[playerId].pocket += GameInstance.GetCurrent().EmeraldsForTake;
                        GameInstance.GetCurrent().EmeraldsForTake = 0;
                    }

                    GameInstance.PlayersPublic[playerId].chest += GameInstance.PlayersPublic[playerId].pocket;
                    GameInstance.PlayersPublic[playerId].status = PlayerStatus.RESTING;
                }

                if( GameInstance.MineNumber > GameInstance.CurrentMineID + 1 )
                {
                    GameInstance.GetCurrent().MineState = MineStatus.VISITED;
                    GameInstance.CurrentMineID++;
                    GameInstance.GetCurrent().MineState = MineStatus.CURRENT;
                    GameInstance.PublicState = GameStatus.ROUND_SUMMARY;
                }
                else
                {
                    GameInstance.GetCurrent().MineState = MineStatus.VISITED;
                    GameInstance.PublicState = GameStatus.FINISHED;
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
                        GameInstance.PlayersPublic[playerId].pocket += GameInstance.GetCurrent().EmeraldsForTake;
                        GameInstance.GetCurrent().EmeraldsForTake = 0;
                    }

                    GameInstance.PlayersPublic[playerId].chest += GameInstance.PlayersPublic[playerId].pocket;
                    GameInstance.PlayersPublic[playerId].status = PlayerStatus.RESTING;
                    isExploring[playerId] = false;
                }


                //selecting card
                var selectedCard = deck.GetRandomCard(RemovedCard, this.GameInstance.GetCurrent().Node, DragonPossible());

                GameInstance.GetCurrent().Node.Add(selectedCard.CardID);

                //If emeralds selected
                if(selectedCard.Type == CardType.ARTIFACT || selectedCard.Type == CardType.EMERALDS)
                {
                    int rest = selectedCard.EmeraldValue % GameInstance.PlayersPublic.Length;
                    int byPlayer = (selectedCard.EmeraldValue - rest) / GameInstance.PlayersPublic.Length;

                    if (rest + GameInstance.GetCurrent().EmeraldsForTake > GameInstance.PlayersPublic.Length)
                    {
                        int forF = rest + GameInstance.GetCurrent().EmeraldsForTake;
                        rest = forF % GameInstance.PlayersPublic.Length;
                        byPlayer += (forF - rest) / GameInstance.PlayersPublic.Length;
                    }

                    foreach (PlayersPublic player in GameInstance.PlayersPublic)
                    {
                        player.pocket += byPlayer;
                    }

                    GameInstance.GetCurrent().EmeraldsForTake = rest;

                    foreach(int playerID in goingPlayer)
                    {
                        playersDecisions[playerID] = PlayerDecision.UNKNOWN;
                    }

                    GameInstance.PublicState = GameStatus.WAITING_FOR_MOVE;
                    GameInstance.RoundID++;
                    return;
                }
                else
                {
                    var resoult = CheckIfRoundIsFinnished(GameInstance.GetCurrent().Node, selectedCard.Type);
                
                    if(resoult.Count > 0 || selectedCard.Type == CardType.DRAGON)
                    {

                        if (resoult.Count > 0)
                            RemovedCard.AddRange(resoult);
                        else
                            RemovedCard.Add(selectedCard.CardID);

                        for(int iter=0; iter < playersDecisions.Length; iter++)
                        {
                            playersDecisions[iter] = PlayerDecision.GO_FURTHER;
                            isExploring[iter] = true;

                        }

                        goingPlayer.ForEach(playerId =>
                        {
                            GameInstance.PlayersPublic[playerId].status = PlayerStatus.DEAD;
                            GameInstance.PlayersPublic[playerId].pocket = 0;
                        });


                        if (GameInstance.MineNumber > GameInstance.CurrentMineID + 1)
                        {
                            GameInstance.GetCurrent().MineState = MineStatus.VISITED;
                            GameInstance.CurrentMineID++;
                            GameInstance.GetCurrent().MineState = MineStatus.CURRENT;
                            GameInstance.PublicState = GameStatus.ROUND_SUMMARY;
                        }
                        else
                        {
                            GameInstance.GetCurrent().MineState = MineStatus.VISITED;
                            GameInstance.PublicState = GameStatus.FINISHED;
                        }


                        LogManager.Print("Round finished by: "+selectedCard.Type.ToString(), "SinglePlayerGameInstance");
                        return;


                    }
                    else
                    {

                        for(int iter = 0; iter<0; iter++)
                        {
                            playersDecisions[iter] = PlayerDecision.UNKNOWN;
                        }

                        GameInstance.PublicState = GameStatus.WAITING_FOR_MOVE;
                        GameInstance.RoundID++;
                        return;
                    }
                
                }

            }

        }

        private bool DragonPossible()
        {
            if (this.gameConfig.DragonsMinimalDeep < 0)
            {
                return false;
            }
            else
            {
                return (this.GameInstance.GetCurrent().Node.Count >= this.gameConfig.DragonsMinimalDeep);
            }
        }

        private void PlayFirstCard()
        {
            var selectedCard = deck.GetRandomCard(RemovedCard, this.GameInstance.GetCurrent().Node, DragonPossible());

            foreach( PlayersPublic player in GameInstance.PlayersPublic)
            {
                player.status = PlayerStatus.EXPLORING;
                player.pocket = 0;
            }

            if(selectedCard.Type == CardType.ARTIFACT || selectedCard.Type == CardType.EMERALDS)
            {
                int rest = selectedCard.EmeraldValue % GameInstance.PlayersPublic.Length;
                int byPlayer = (selectedCard.EmeraldValue - rest) / GameInstance.PlayersPublic.Length;
                
                if(rest + GameInstance.GetCurrent().EmeraldsForTake > GameInstance.PlayersPublic.Length)
                {
                    int forF = rest + GameInstance.GetCurrent().EmeraldsForTake;
                    rest = forF % GameInstance.PlayersPublic.Length;
                    byPlayer += (forF - rest) / GameInstance.PlayersPublic.Length;
                }

                foreach (PlayersPublic player in GameInstance.PlayersPublic)
                {
                    player.pocket += byPlayer;
                }

                GameInstance.GetCurrent().EmeraldsForTake += rest;
            }

            GameInstance.GetCurrent().Node.Add(selectedCard.CardID);

            for(int iter = 0; iter < playersDecisions.Length; iter++)
            {
                playersDecisions[iter] = PlayerDecision.UNKNOWN;
                isExploring[iter] = true;
            }

            GameInstance.PublicState = GameStatus.WAITING_FOR_MOVE;
            GameInstance.RoundID++;
        }

        private void LetBotsDecide(GameInstance game) { 

            for(int iter = 0; iter < botPlayer.Length; iter++)
            {
                if( game.PlayersPublic[iter].status == PlayerStatus.EXPLORING) playersDecisions[iter] = botPlayer[iter].DecisionStrategy.makeDecision(game, iter);
            }

        }

        public void MakeDecision(bool decision)
        {
            LogManager.Print("Decision made: "+decision, "SinglePlayerGameInstance");
            playersDecisions[GameInstance.PlayersPublic.Length - 1] = (decision ? PlayerDecision.GO_FURTHER : PlayerDecision.GO_BACK);
        }

        public void Start(Action<GameInstance> callback)
        {
            this.UIupdate = callback;
            this.abortGame = false;
        }
        
        public void StopGame()
        {
            this.abortGame = true;
            this.GameInstance.PublicState = GameStatus.FINISHED;
        }

        public void ScheduleNextMove(int seconds)
        {
            LogManager.Print("waiting", "SinglePlayerGameInstance");
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                if (!this.abortGame)
                {
                    LogManager.Print("doing move;", "SinglePlayerGameInstance");
                    if(GameInstance.PublicState == GameStatus.WAITING_FOR_MOVE)
                        LetBotsDecide(GameInstance);

                    string dec = "";
                    foreach(PlayerDecision decision in playersDecisions)
                    {
                        dec += decision.ToString() + "; ";
                    }
                    Console.WriteLine(dec);
                    Next();
                }

                return false;
            });
        }


        private List<int> CheckIfRoundIsFinnished(List<int> usedCard, CardType selectedType)
        {
            List<int> cardtoRemove = new List<int>();

            cardtoRemove = usedCard.FindAll(cardID => deck.GetCardOfID(cardID).Type == selectedType);

            if(cardtoRemove.Count == 3)
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
