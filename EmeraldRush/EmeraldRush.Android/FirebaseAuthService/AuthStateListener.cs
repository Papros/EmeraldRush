using System;
using Firebase.Auth;
using Java.Interop;
using static Firebase.Auth.FirebaseAuth;

namespace EmeraldRush.Droid.FirebaseAuthService
{
    class AuthStateListener : Java.Lang.Object, IAuthStateListener 
    {
        public new IntPtr Handle => throw new NotImplementedException();

        public new int JniIdentityHashCode => throw new NotImplementedException();

        public new JniObjectReference PeerReference => throw new NotImplementedException();

        public new JniPeerMembers JniPeerMembers => throw new NotImplementedException();

        public JniManagedPeerStates JniManagedPeerState => throw new NotImplementedException();

        public new void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Disposed()
        {
            throw new NotImplementedException();
        }

        public void DisposeUnlessReferenced()
        {
            throw new NotImplementedException();
        }

        public void Finalized()
        {
            throw new NotImplementedException();
        }

        public void OnAuthStateChanged(FirebaseAuth auth)
        {
            throw new NotImplementedException();
        }

        public void SetJniIdentityHashCode(int value)
        {
            throw new NotImplementedException();
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
            throw new NotImplementedException();
        }

        public void SetPeerReference(JniObjectReference reference)
        {
            throw new NotImplementedException();
        }

        public new void UnregisterFromRuntime()
        {
            throw new NotImplementedException();
        }
    }
}