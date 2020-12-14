using EmeraldRush.Model.FirebaseModel;
using System;

namespace EmeraldRush.Model.GameManager
{
    internal interface IGameManager
    {
        void MakeDecision(bool decision, int PlayerID, string GameUID);

        string GetUserUID();
        void Subscribe(Action<GameInstance> callback);
    }
}
