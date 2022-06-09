using DllSky.Services;
using GOALS.GlobalManagers;
using GOALS.Windows.Enums;
using Photon.Pun;
using Photon.Realtime;
using Unity.Services.RemoteConfig;

namespace GOALS.Windows.ApplicationLoading.LoadingSteps
{
    public class PhotonConnectStep : LoadingStepBase
    {
        public override EnumLoadingStep Step => EnumLoadingStep.PhotonConnect;


        public PhotonConnectStep(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep) : base(completeNextStep, failedNextStep)
        {

        }


        protected override void CustomStart()
        {
            DoStep();
        }

        protected override void CustomComplete()
        {
            Unsubscribe();
        }

        protected override void CustomFailed(string error)
        {
            Unsubscribe();
        }


        private void DoStep()
        {
            if (Utilities.CheckForInternetConnection())
            {
                Subscribe();

                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                Failed("Not Internet Connection");
            }
        }

        private void Subscribe()
        {
            var callbacks = ComponentLocator.Resolve<PhotonCallbacks>();

            callbacks.EventOnConnectedToMaster += OnConnectedToMasterHandler;
            callbacks.EventOnDisconnected += OnDisconnectedHandler;
            callbacks.EventOnJoinedLobby += OnJoinedLobbyHandler;
            callbacks.EventOnLeftLobby += OnLeftLobbyHandler;
        }

        private void Unsubscribe()
        {
            var callbacks = ComponentLocator.Resolve<PhotonCallbacks>();

            callbacks.EventOnConnectedToMaster -= OnConnectedToMasterHandler;
            callbacks.EventOnDisconnected -= OnDisconnectedHandler;
            callbacks.EventOnJoinedLobby -= OnJoinedLobbyHandler;
            callbacks.EventOnLeftLobby -= OnLeftLobbyHandler;
        }

        private void OnConnectedToMasterHandler()
        {
            ChangeProgress(0.5f);
            PhotonNetwork.JoinLobby();
        }

        private void OnDisconnectedHandler(DisconnectCause cause)
        {
            Failed($"cause {cause}");
        }

        private void OnJoinedLobbyHandler()
        {
            ChangeProgress(1.0f);
        }

        private void OnLeftLobbyHandler()
        {
            Failed($"Left Room");
        }
    }
}
