using System;
using System.Collections.Generic;
using System.Text;

namespace EmeraldRush.Model.FirebaseModel
{
    class QueueToken
    {
        public string UserUID { get; set; }
        public string GameMode { get; set; }

        public QueueToken(string UserUID, string GameMode)
        {
            this.UserUID = UserUID;
            this.GameMode = GameMode;
        }
    }
}
