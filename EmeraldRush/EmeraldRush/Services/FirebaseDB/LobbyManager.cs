using EmeraldRush.Model.FirebaseModel;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldRush.Services.FirebaseDB
{
    class LobbyManager
    {
        public static IObservable<FirebaseEvent<Player>> playerInstance;
        public static IDisposable unsubObject;
        public enum GAME_TYPE
        {
            GAME_2_PLAYERS, 
            GAME_4_PLAYERS, 
            GAME_8_PLAYERS
        }

        public static async Task<bool> SignSelfToGameList(GAME_TYPE type, String UserUID)
        {
            if(UserUID != string.Empty)
            {
                Player player = new Player(string.Empty, "Adventurer",UserUID);
                Console.WriteLine("Sending player to list...");

                try
                {
                    await FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.USER_LIST).Child(UserUID).Child("user").PatchAsync(player);
                    Console.WriteLine("send.");

                    
                    playerInstance = FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.USER_LIST).Child(UserUID).AsObservable<Player>();
                    
                    unsubObject = playerInstance.Subscribe(Job =>
                    {
                        
                        Console.WriteLine("JAKIEŚ ZMIANY...");
                        
                        if (Job.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate && !string.IsNullOrEmpty(Job.Object.GameUID))
                        {
                           // FirebaseGameManager.SubscribeToGame(Job.Object.GameUID);
                            Console.WriteLine("Znaleziono gre: " + Job.Object.GameUID);
                            playerInstance = null;
                            
                        }

                    });

                    Console.WriteLine("Subscribed to: " + AplicationConstants.USER_LIST + "/" + UserUID);
                    QueueToken token = new QueueToken(UserUID, type.ToString());
                    await FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.QUEUE).Child(UserUID).PatchAsync(token);
                    Console.WriteLine("User set in queue: " + type.ToString());
                    return true;

                }catch( Firebase.Database.FirebaseException ex)
                {
                    Console.WriteLine("NULL REFERENCE: "+ex.Message);
                    return false;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("NULL REFERENCE: " + ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
