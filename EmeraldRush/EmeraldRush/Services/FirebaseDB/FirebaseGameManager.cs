using EmeraldRush.Model.FirebaseModel;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Services.FirebaseDB
{
    class FirebaseGameManager
    {
        public static IObservable<FirebaseEvent<Game>> gameObservable;
        public static void SubscribeToGame(string GameUID)
        {
            gameObservable = FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.GAME_LIST).Child(AplicationConstants.PUBLIC_GAME_DATA).AsObservable<Game>();

            gameObservable.Subscribe(Job => { 
                
                if(Job.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                {
                    
                }

            });
        }

        public static void Unsubscribe()
        {
            if(gameObservable != null)
            {
                
            }
        }


    }
}
