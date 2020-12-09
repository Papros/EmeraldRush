using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmeraldRush.Services.FirebaseAuthService
{
    class FirebaseAuthManager
    {
        public static async Task<string> Login()
        {
            return await DependencyService.Get<IFirebaseAuthentication>().LoginAnonymous();
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
            else if( await Login() != string.Empty)
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
