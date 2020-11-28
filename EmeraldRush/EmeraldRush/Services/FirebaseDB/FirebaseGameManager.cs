using EmeraldRush.Model.FirebaseModel;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System;
using System.Collections.Generic;
using System.Text;
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
            this.GameUID = GameUID;
        }

        private string GameUID;
        public IDisposable gameDisposble;

        private GameInstance gameCache;

        public void SubscribeToGame(string gameUID = "")
        {
            if(gameUID != "")
            {
                this.GameUID = gameUID;
            }

            gameDisposble = FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.GAME_LIST).Child(GameUID).Child(AplicationConstants.GAME_NODE).Child(AplicationConstants.PUBLIC_GAME_DATA).AsObservable<GameInstance>().Subscribe(Job => 
            { 
            
                if(Job.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate && Job.Object != null)
                {
                    Console.WriteLine( "Update in:" + Job.Object.GameUID );
                    MessagingCenter.Send<FirebaseGameManager, GameInstance>(this, AplicationConstants.GAME_UPDATE_MSG, Job.Object);
                    this.gameCache = Job.Object;
                }

            });
        }

        public async Task<GameInstance> GetGame()
        {
            this.gameCache = (await FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.GAME_LIST).Child(GameUID).Child(GameUID).Child(AplicationConstants.GAME_NODE).Child(AplicationConstants.PUBLIC_GAME_DATA).OnceSingleAsync<GameInstance>());
            return gameCache;
        }

        public GameInstance GetCashedGame()
        {
            return gameCache;
        }

        public void Unsubscribe()
        {
            if(gameDisposble!= null)
            {
                gameDisposble.Dispose();
            }
        }


    }
}
