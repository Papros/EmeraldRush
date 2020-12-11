using EmeraldRush.Model.FirebaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.GameManager
{
    public interface IGameManager
    {
        public Action<GameInstance> GameUpdateCallback { get; set; }
        void MakeDecision(bool decision, int PlayerID = -1, string GameUID = "");

        void Subscribe();
    }
}
