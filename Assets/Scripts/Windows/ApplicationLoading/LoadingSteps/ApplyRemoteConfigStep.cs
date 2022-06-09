using GOALS.Windows.Enums;

namespace GOALS.Windows.ApplicationLoading.LoadingSteps
{
    public class ApplyRemoteConfigStep : LoadingStepBase
    {
        public override EnumLoadingStep Step => EnumLoadingStep.ApplyRemoteConfig;


        public ApplyRemoteConfigStep(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep) : base(completeNextStep, failedNextStep)
        {

        }


        protected override void CustomStart()
        {
            //TODO...
            ChangeProgress(1.0f);
        }
    }
}
