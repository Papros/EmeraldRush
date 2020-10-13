using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.Services.FirebaseAuthService
{
    class FirebaseAuthManager
    {
        public static async Task<bool> Login()
        {
            return await DependencyService.Get<IFirebaseAuthentication>().LoginAnonymous() != string.Empty;
        }

        public static bool IsLogged()
        {
            return DependencyService.Get<IFirebaseAuthentication>().IsSignIn();
        }

        public static string GetUserUID()
        {
            return DependencyService.Get<IFirebaseAuthentication>().GetUserUID();
        }

        public static async Task<string> LoginAndGetUID()
        {
            if (IsLogged())
            {
                return GetUserUID();
            }
            else if(await Login())
            {
                return GetUserUID();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
