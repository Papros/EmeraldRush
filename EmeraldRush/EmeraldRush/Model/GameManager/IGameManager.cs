using EmeraldRush.Model.FirebaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.GameManager
{
    internal interface IGameManager
    {
        void MakeDecision(bool decision, int PlayerID, string GameUID);

        string GetUserUID();
        void Subscribe(Action<GameInstance> callback);
    }
}
