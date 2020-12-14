using EmeraldRush.Model.FirebaseModel;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.Services.FirebaseDB
{
    class FirebaseGameManager
    {
        private static FirebaseGameManager instance = null;

        public static FirebaseGameManager GetInstance()
        {
            return instance;
        }

        public static FirebaseGameManager Initialize(string GameUID)
        {
            instance = new FirebaseGameManager(GameUID);
            return instance;
        }

        public static void Dispose()
        {
            instance = null;
        }

        private FirebaseGameManager(string GameUID)
        {
            this.gameUID = GameUID;
        }

        private string gameUID;
        private IDisposable gameDisposble;
        private GameInstance gameCache;

        public bool SubscribeToGame(string gameUID = "")
        {
            if (gameUID != "")
            {
                this.gameUID = gameUID;
            }

            gameDisposble = FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.GAME_LIST).Child(gameUID).Child(AplicationConstants.GAME_NODE).Child(AplicationConstants.PUBLIC_GAME_DATA).AsObservable<GameInstance>().Subscribe(Job =>
            {

                LogManager.Print("found some updates", "FirebaseGameManager");
                if (Job.Object != null)
                {
                    if (Job.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        LogManager.Print("Game update", "FirebaseGameManager");
                        MessagingCenter.Send<FirebaseGameManager, GameInstance>(this, AplicationConstants.GAME_UPDATE_MSG, Job.Object);
                        gameCache = Job.Object;
                    }
                    else
                    {
                        LogManager.Print("Not UpdateOrInsert", "FirebaseGameManager");
                    }
                }
                else
                {
                    LogManager.Print("Object null", "FirebaseGameManager");
                }

            });
            LogManager.Print("game subscribed..");
            return true;

        }

        public async Task<GameInstance> GetGame()
        {
            gameCache = (await FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.GAME_LIST).Child(gameUID).Child(gameUID).Child(AplicationConstants.GAME_NODE).Child(AplicationConstants.PUBLIC_GAME_DATA).OnceSingleAsync<GameInstance>());
            return gameCache;
        }

        public GameInstance GetCashedGame()
        {
            return gameCache;
        }

        public void Unsubscribe()
        {
            if (gameDisposble != null)
            {
                gameDisposble.Dispose();
            }
        }


    }
}
