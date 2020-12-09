using System;
using System.Threading.Tasks;
using EmeraldRush.Droid.Services;
using EmeraldRush.Services.FirebaseAuthService;
using Firebase.Auth;

namespace EmeraldRush.Droid.FirebaseAuthService
{

    class FirebaseAuthentication : IFirebaseAuthentication
    {
        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public string GetUserUID()
        {
            var user = FirebaseAuth.Instance.CurrentUser;

            return user?.Uid;
        }

        public async Task<string> LoginAnonymous()
        {
            try
            {
                var authResoult = await FirebaseAuth.Instance.SignInAnonymouslyAsync();
                var token = await authResoult.User.GetIdTokenAsync(true);


                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                LogManager.Print("FirebaseAuthInvlidUserException: " + e, "droid.FirebaseAuthentication");
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                LogManager.Print("FirebaseAuthInvalidCredentialsException: " + e, "droid.FirebaseAuthentication");
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}