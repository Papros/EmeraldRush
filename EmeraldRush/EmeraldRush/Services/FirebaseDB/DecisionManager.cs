using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace EmeraldRush.Services.FirebaseDB
{
    class DecisionManager
    {
        public static async Task SendDecision(string GameUID, int playerId, bool goingFurther)
        {
            await FirebaseManager.GetInstance().GetClient()
                .Child(AplicationConstants.GAME_LIST)
                .Child(GameUID)
                .Child(AplicationConstants.GAME_NODE)
                .Child(AplicationConstants.PRIVATE_GAME_DATA)
                .Child(playerId.ToString())
                .Child(AplicationConstants.DECISION_PROPERTIES).PatchAsync(goingFurther);
        }
    }
}
