using GOALS.Windows.ApplicationLoading.LoadingSteps;
using GOALS.Windows.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace GOALS.Windows.ApplicationLoading
{
    public class ApplicationLoadingWindow : WindowTemplate
    {
        public static new string PathPrefab = "Windows/Loading/ApplicationLoadingWindow";

        private const EnumLoadingStep FIRST_LOADING_STEP = EnumLoadingStep.InitializeUnityService;
        private const EnumLoadingStep LAST_LOADING_STEP = EnumLoadingStep.LastStep;

        private Dictionary<EnumLoadingStep, LoadingStepBase> _steps;
        private float _maxProgress;


        public override void Initialize(object data)
        {
            base.Initialize(data);

            _steps = CreateLoadingSteps();

            Subscribe();
            StartLoadingStep(FIRST_LOADING_STEP);
        }


        protected override void CustomClose(bool result)
        {
            Unsubscribe();
        }


        private Dictionary<EnumLoadingStep, LoadingStepBase> CreateLoadingSteps()
        {
            _maxProgress = 5.0f;

            return new Dictionary<EnumLoadingStep, LoadingStepBase>
            {
                { EnumLoadingStep.InitializeUnityService, new InitializeUnityServiceStep(EnumLoadingStep.LoadRemoteConfig, EnumLoadingStep.LoadLocalConfig) },
                { EnumLoadingStep.LoadRemoteConfig, new LoadRemoteConfigStep(EnumLoadingStep.ApplyRemoteConfig, EnumLoadingStep.ApplyLocalConfig) },
                { EnumLoadingStep.ApplyRemoteConfig, new ApplyRemoteConfigStep(EnumLoadingStep.PhotonConnect, EnumLoadingStep.LastStep) },
                { EnumLoadingStep.PhotonConnect, new PhotonConnectStep(EnumLoadingStep.LastStep, EnumLoadingStep.LastStep)},

                //...

                { EnumLoadingStep.LastStep, new LastStep(EnumLoadingStep.NA, EnumLoadingStep.NA)},
            };
        }

        private void StartLoadingStep(EnumLoadingStep stepType)
        {
            if (_steps.ContainsKey(stepType))
                _steps[stepType].Start();
            else
                Debug.LogError($"[ApplicationLoadingWindow] Not found step with type {stepType}");
        }

        private bool CheckLastStep(EnumLoadingStep stepType)
        {
            return stepType == LAST_LOADING_STEP;
        }

        private void Subscribe()
        {
            foreach (var step in _steps)
            {
                step.Value.OnStart += OnStartHandler;
                step.Value.OnChangeProgress += OnChangeProgressHandler;
                step.Value.OnComplete += OnCompleteHandler;
                step.Value.OnFailed += OnFailedHandler;
            }
        }

        private void Unsubscribe()
        {
            foreach (var step in _steps)
            {
                step.Value.OnStart -= OnStartHandler;
                step.Value.OnChangeProgress -= OnChangeProgressHandler;
                step.Value.OnComplete -= OnCompleteHandler;
                step.Value.OnFailed -= OnFailedHandler;
            }
        }

        private void OnStartHandler(LoadingStepBase step)
        { 
        
        }

        private void OnChangeProgressHandler(LoadingStepBase step, float progress)
        {
            Debug.LogError($"[ApplicationLoadingWindow] OnChangeProgressHandler {step.GetType()}, progress {progress}");
        }

        private void OnCompleteHandler(LoadingStepBase step)
        {
            if (CheckLastStep(step.Step))
                ;
            else
                StartLoadingStep(step.completeNextStep);
        }

        private void OnFailedHandler(LoadingStepBase step, string error)
        {
            if (CheckLastStep(step.Step))
                ;
            else
                StartLoadingStep(step.failedNextStep);
        }
    }
}
