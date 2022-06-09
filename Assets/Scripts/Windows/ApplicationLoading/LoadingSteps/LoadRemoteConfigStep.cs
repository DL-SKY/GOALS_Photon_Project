using DllSky.Services;
using GOALS.GlobalManagers;
using GOALS.Windows.Enums;
using Unity.Services.RemoteConfig;

namespace GOALS.Windows.ApplicationLoading.LoadingSteps
{
    public class LoadRemoteConfigStep : LoadingStepBase
    {
        public struct UserAttributes { };
        public struct AppAttributes { };


        public override EnumLoadingStep Step => EnumLoadingStep.LoadRemoteConfig;


        public LoadRemoteConfigStep(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep) : base(completeNextStep, failedNextStep)
        {

        }


        protected override void CustomStart()
        {
            DoStep();
        }


        private void DoStep()
        {
            if (Utilities.CheckForInternetConnection())
            {
                var appManager = ComponentLocator.Resolve<ApplicationManager>();
                var id = appManager?.GetConfig()?.remoteConfigEnvironmentID ?? "";
                RemoteConfigService.Instance.SetEnvironmentID(id);
                RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());

                RemoteConfigService.Instance.FetchCompleted += FetchCompletedHandler;
            }
            else
            {
                Failed("Not Internet Connection");
            }
        }

        private void FetchCompletedHandler(ConfigResponse configResponse)
        {
            RemoteConfigService.Instance.FetchCompleted -= FetchCompletedHandler;

            switch (configResponse.status)
            {
                case ConfigRequestStatus.None:
                case ConfigRequestStatus.Pending:
                case ConfigRequestStatus.Failed:
                    Failed($"ConfigRequestStatus: {configResponse.status}");
                    return;
            }

            switch (configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                case ConfigOrigin.Cached:
                    Failed($"requestOrigin: {configResponse.requestOrigin}");
                    return;
                case ConfigOrigin.Remote:
                    Complete();
                    return;
            }
        }
    }
}
