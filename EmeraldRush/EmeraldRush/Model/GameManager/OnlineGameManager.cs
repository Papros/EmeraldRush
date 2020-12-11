using EmeraldRush.Model.GameEnum;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldRush.Model.GameManager
{
    class OnlineGameManager : IGameManager
    {
        public void Initilize()
        {
            throw new NotImplementedException();
        }

        public void MakeDecision(bool decision, int PlayerID = -1, string GameUID = "")
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
    }
}
