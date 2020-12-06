using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.Services.FirebaseDB
{
    class LobbyManager
    {
        public static IDisposable playerDisposble;
        public static async Task<bool> SignSelfToGameList(GameMode type, String UserUID)
        {
            if(UserUID != string.Empty)
            {
                Player player = new Player(string.Empty, "Adventurer",UserUID);

                try
                {
                    await FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.USER_LIST).Child(UserUID).Child("user").PatchAsync(player);

                    playerDisposble = FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.USER_LIST).Child(UserUID).AsObservable<Player>().Subscribe(Job =>
                    {
                        if (Job.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate && !string.IsNullOrEmpty(Job.Object.GameUID))
                        {
                            if( FirebaseGameManager.Initialize(Job.Object.GameUID).SubscribeToGame())
                            {
                                LogManager.Print("Game subscribed", "LobbyManager");
                                MessagingCenter.Send<LobbyManager>(new LobbyManager(), AplicationConstants.GAME_FOUND_MSG);
                                UnsubscribePlayerAccount();
                            }
                            else
                            {
                                LogManager.Print("Game subscribe error", "LobbyManager");
                            }
                        }

                    });

                    QueueToken token = new QueueToken(UserUID, type.ToString());
                    await FirebaseManager.GetInstance().GetClient().Child(AplicationConstants.QUEUE).Child(UserUID).PatchAsync(token);

                    return true;

                }catch( Firebase.Database.FirebaseException ex)
                {
                    LogManager.Print("Account subscribe FirebaseException: " + ex, "LobbyManager");
                    return false;
                }
                catch (System.Exception ex)
                {
                    LogManager.Print("Account subscribe Exception: " + ex, "LobbyManager");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public static void UnsubscribePlayerAccount()
        {
            if(playerDisposble != null)
            {
                playerDisposble.Dispose();
            }
        }
    }
}
