using EmeraldRush.Services.FirebaseAuthService;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldRush.ViewModels.Lobby
{
    class OnlineGameLobbyViewModel
    {
        public bool WaitingForGame { get; private set; }
        public string LobbyStatus { get; private set; }

        public OnlineGameLobbyViewModel()
        {
            WaitingForGame = false;
            LobbyStatus = "First, select game type:";
        }

        public async Task AwaitForGame(int type)
        {
            string userUID = await FirebaseAuthManager.LoginAndGetUID();
            EmeraldRush.Services.LogManager.Print("Get user UID: "+userUID);

            switch (type)
            {
                case 2: await LobbyManager.SignSelfToGameList(LobbyManager.GAME_TYPE.GAME_2_PLAYERS,userUID); break;
                case 4: await LobbyManager.SignSelfToGameList(LobbyManager.GAME_TYPE.GAME_4_PLAYERS,userUID); break;
                case 8: await LobbyManager.SignSelfToGameList(LobbyManager.GAME_TYPE.GAME_8_PLAYERS,userUID); break;
            }

            EmeraldRush.Services.LogManager.Print("Signed...");
            LobbyStatus = "Waiting for more players...";
        }
    }
}
