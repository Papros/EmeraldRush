using EmeraldRush.Model.AIMode.Game;
using EmeraldRush.Model.FirebaseModel;
using System;

namespace EmeraldRush.Model.GameManager
{
    class SinglePlayerGameManager : IGameManager
    {
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
            callback(GameInstance.PublicGameData);
            GameInstance.Start(callback);
            GameInstance.Next();
        }

    }
}
