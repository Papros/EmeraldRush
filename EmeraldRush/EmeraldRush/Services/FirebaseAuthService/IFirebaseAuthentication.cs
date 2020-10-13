using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldRush.Services.FirebaseAuthService
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginAnonymous();
        bool SignOut();
        bool IsSignIn();
        string GetUserUID();
    }
}
