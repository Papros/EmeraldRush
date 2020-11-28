using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmeraldRush.Model.FirebaseModel;
using EmeraldRush.Model.GameEnum;
using Firebase.Database.Query;

namespace EmeraldRush.Services.FirebaseDB
{
    class DecisionManager
    {
        public static async Task SendDecision(string GameUID, int playerId, PlayerDecision goingFurtherDecision)
        {

            DecisionToken decision = new DecisionToken(goingFurtherDecision);
            LogManager.Print("Send decision " + goingFurtherDecision.ToString()+":> "+decision.Decision, "DecisionManager");

            await FirebaseManager.GetInstance().GetClient()
                .Child(AplicationConstants.GAME_LIST)
                .Child(GameUID)
                .Child(AplicationConstants.GAME_NODE)
                .Child(AplicationConstants.PRIVATE_GAME_DATA)
                .Child(AplicationConstants.PLAYERS_PRIVATE_NODE)
                .Child(playerId.ToString()).PatchAsync(decision);
                //.Child(AplicationConstants.DECISION_PROPERTIES).PatchAsync(goingFurtherDecision);
        }
    }
}
