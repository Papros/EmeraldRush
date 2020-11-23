using EmeraldRush.Services;
using EmeraldRush.Services.FirebaseAuthService;
using EmeraldRush.Services.FirebaseDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.ViewModels.Lobby
{
    class OnlineGameLobbyViewModel : BaseViewModel
    {
        public bool WaitingForGame { get; private set; }
        public string LobbyStatus { get; private set; }

        public OnlineGameLobbyViewModel()
        {
            WaitingForGame = false;
            LobbyStatus = "Select you today`s game type!";
        }

        public async Task SignInToPlayersQueue(int type, Action gameFoundCallbck)
        {
            string userUID = await FirebaseAuthManager.LoginAndGetUID();
            EmeraldRush.Services.LogManager.Print("Get user UID: "+userUID);

            MessagingCenter.Subscribe<LobbyManager>(this, AplicationConstants.GAME_FOUND_MSG, (sender) =>
            {
                LobbyStatus = "Game found, connecting...";
                MessagingCenter.Unsubscribe<LobbyManager>(this, AplicationConstants.GAME_FOUND_MSG);
                Console.WriteLine("Message recived");
                gameFoundCallbck.Invoke();
            });

            bool resoult = false;

            switch (type)
            {
                case 2: resoult = await LobbyManager.SignSelfToGameList(Model.GameEnum.GameMode.GAME_2_PLAYERS,userUID); break;
                case 4: resoult = await LobbyManager.SignSelfToGameList(Model.GameEnum.GameMode.GAME_4_PLAYERS,userUID); break;
                case 8: resoult = await LobbyManager.SignSelfToGameList(Model.GameEnum.GameMode.GAME_8_PLAYERS,userUID); break;
            }

            if (!resoult)
            {
                MessagingCenter.Unsubscribe<LobbyManager>(this, AplicationConstants.GAME_FOUND_MSG);
            }

            EmeraldRush.Services.LogManager.Print("Signed.");
            LobbyStatus = "Looking for another adventurers...";

            
        }

    }
}
