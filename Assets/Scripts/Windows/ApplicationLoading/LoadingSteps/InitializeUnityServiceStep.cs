using GOALS.Windows.Enums;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;

namespace GOALS.Windows.ApplicationLoading.LoadingSteps
{
    public class InitializeUnityServiceStep : LoadingStepBase
    {
        public override EnumLoadingStep Step => EnumLoadingStep.InitializeUnityService;


        public InitializeUnityServiceStep(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep) : base(completeNextStep, failedNextStep)
        { 
        
        }


        protected override void CustomStart()
        {
            DoStep();            
        }


        async private void DoStep()
        {
            if (Utilities.CheckForInternetConnection())
            {
                await UnityServices.InitializeAsync();

                if (!AuthenticationService.Instance.IsSignedIn)
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();

                Complete();
            }
            else
            {
                Failed("Not Internet Connection");
            }
        }
    }
}
