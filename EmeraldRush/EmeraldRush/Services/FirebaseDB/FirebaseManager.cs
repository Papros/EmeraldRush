using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldRush.Services.FirebaseDB
{
    class FirebaseManager
    {
        private FirebaseClient DatabaseClient;

        private static FirebaseManager instance;

        public static FirebaseManager GetInstance()
        {
            if (instance == null)
            {
                instance = new FirebaseManager();
            }

            return instance;
        }

        private FirebaseManager()
        {
            FirebaseOptions options = new FirebaseOptions { AuthTokenAsyncFactory = async () => await FirebaseAuthService.FirebaseAuthManager.Login() };

            this.DatabaseClient = new FirebaseClient(AplicationConstants.FIRABASE_URL_ADRESS, options );
        }

        public FirebaseClient GetClient()
        {
            return this.DatabaseClient;
        }


    }
}
