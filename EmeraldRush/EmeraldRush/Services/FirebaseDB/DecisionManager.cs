using EmeraldRush.Model.GameEnum;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace EmeraldRush.Services.FirebaseDB
{
    class DecisionManager
    {
        public static async Task SendDecision(string GameUID, int playerId, PlayerDecision goingFurtherDecision)
        {

            //DecisionToken decision = new DecisionToken(goingFurtherDecision);
            LogManager.Print("Send decision " + goingFurtherDecision.ToString() + ":> " + goingFurtherDecision, "DecisionManager");

            await FirebaseManager.GetInstance().GetClient()
                .Child(AplicationConstants.GAME_LIST)
                .Child(GameUID)
                .Child(AplicationConstants.GAME_NODE)
                .Child(AplicationConstants.PRIVATE_GAME_DATA)
                .Child(AplicationConstants.PLAYERS_PRIVATE_NODE)
                .Child(playerId.ToString())
                .Child(AplicationConstants.DECISION_PROPERTIES).PutAsync(goingFurtherDecision);

        }
    }
}
