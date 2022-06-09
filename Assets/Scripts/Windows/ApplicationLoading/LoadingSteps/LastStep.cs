using DllSky.Services;
using GOALS.GlobalManagers;
using GOALS.Windows.Enums;
using UnityEngine;

namespace GOALS.Windows.ApplicationLoading.LoadingSteps
{
    public class LastStep : LoadingStepBase
    {
        public override EnumLoadingStep Step => EnumLoadingStep.LastStep;

        private ScenesManager _scenesManager;


        public LastStep(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep) : base(completeNextStep, failedNextStep)
        {

        }


        protected override void CustomStart()
        {
            _scenesManager = ComponentLocator.Resolve<ScenesManager>();

            _scenesManager.LoadSceneAsync(Scenes.EnumSceneType.MainMenu);
            _scenesManager.OnAsyncLoadUpdated += OnUpdatedHandler;
            _scenesManager.OnAsyncLoadCompleted += OnCompletedHandler;
        }


        private void OnUpdatedHandler(AsyncOperation operation)
        {
            ChangeProgress(operation.progress);
        }

        private void OnCompletedHandler(AsyncOperation operation)
        {
            _scenesManager.OnAsyncLoadUpdated -= OnUpdatedHandler;
            _scenesManager.OnAsyncLoadCompleted -= OnCompletedHandler;

            ChangeProgress(operation.progress);
        }
    }
}
