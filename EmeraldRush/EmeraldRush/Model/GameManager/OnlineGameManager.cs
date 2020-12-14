using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using EmeraldRush.Services;
using EmeraldRush.Services.FirebaseAuthService;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.Model.GameManager
{
    class OnlineGameManager : IGameManager
    {

        public void MakeDecision(bool decision, int PlayerID, string GameUID)
        {
            if (decision)
            {
                Task.Run(() => DecisionManager.SendDecision(GameUID, PlayerID, PlayerDecision.GO_FURTHER));
            }
            else
            {
                Task.Run(() => DecisionManager.SendDecision(GameUID, PlayerID, PlayerDecision.GO_BACK));
            }
        }

        public string GetUserUID()
        {
            return FirebaseAuthManager.GetUserUID();
        }

        public void Subscribe(Action<GameInstance> notify)
        {
            MessagingCenter.Subscribe<FirebaseGameManager, GameInstance>(this, AplicationConstants.GAME_UPDATE_MSG, (callback, data) =>
            {
                if (data != null)
                {
                    LogManager.Print("Game view updated.", "OnlineGameManager");
                    notify(data);

                }
                else
                {
                    LogManager.Print("Null gameInstance", "OnlineGameManager");
                }

            });
        }

    }
}
