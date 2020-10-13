using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EmeraldRush.Services.FirebaseAuthService;
using Firebase;
using Firebase.Auth;

namespace EmeraldRush.Droid.FirebaseAuthService
{
    
    class FirebaseAuthentication : IFirebaseAuthentication
    {
        public bool IsSignIn()
        {
            var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }

        public string GetUserUID()
        {
            var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;

            return user?.Uid;
        }

        public async Task<string> LoginAnonymous()
        {
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.SignInAnonymouslyAsync();
                var token = await user.User.GetIdTokenAsync(true);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }

        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}