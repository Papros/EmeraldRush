using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.FirebaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.GameManager
{
    class SinglePlayerGameManager : IGameManager
    {
        Action<GameInstance> callback;
        SinglePlayerGameInstance GameInstance;

        public SinglePlayerGameManager(SinglePlayerGameConfig config)
        {
            GameInstance = new SinglePlayerGameInstance(config);
        }

        public string GetUserUID()
        {
            return GameInstance.GetUserUID();
        }

        public void MakeDecision(bool decision, int PlayerID, string GameUID)
        {
            GameInstance.MakeDecision(decision);
        }

        public void Subscribe(Action<GameInstance> callback)
        {
            this.callback = callback;
            callback(GameInstance.GameInstance);
            GameInstance.Start(callback);
            GameInstance.Next();
        }

    }
}
