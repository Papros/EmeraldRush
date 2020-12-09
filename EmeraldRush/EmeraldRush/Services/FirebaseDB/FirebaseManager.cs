using Firebase.Database;

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
