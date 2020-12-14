using Firebase.Database;

namespace EmeraldRush.Services.FirebaseDB
{
    class FirebaseManager
    {
        private FirebaseClient databaseClient;

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

            databaseClient = new FirebaseClient(AplicationConstants.FIRABASE_URL_ADRESS, options);
        }

        public FirebaseClient GetClient()
        {
            return databaseClient;
        }


    }
}
