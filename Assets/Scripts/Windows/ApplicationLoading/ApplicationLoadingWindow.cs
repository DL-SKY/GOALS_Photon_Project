using GOALS.Windows.ApplicationLoading.LoadingSteps;
using GOALS.Windows.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace GOALS.Windows.ApplicationLoading
{
    public class ApplicationLoadingWindow : WindowTemplate
    {
        public static new string PathPrefab = "Windows/Loading/ApplicationLoadingWindow";

        private Dictionary<EnumLoadingStep, LoadingStepBase> _steps;


        public override void Initialize(object data)
        {
            base.Initialize(data);

            _steps = CreateLoadingSteps();

            Subscribe();
            StartLoading(EnumLoadingStep.InitializeUnityService);
        }


        protected override void CustomClose(bool result)
        {
            Unsubscribe();
        }


        private Dictionary<EnumLoadingStep, LoadingStepBase> CreateLoadingSteps()
        {
            return new Dictionary<EnumLoadingStep, LoadingStepBase>
            {
                { EnumLoadingStep.InitializeUnityService, new InitializeUnityServiceStep(EnumLoadingStep.NA, EnumLoadingStep.NA)},
                //...
            };
        }

        private void StartLoading(EnumLoadingStep stepType)
        {
            if (_steps.ContainsKey(stepType))
                _steps[stepType].Start();
            else
                Debug.LogError($"[ApplicationLoadingWindow]Not found step with type {stepType}");
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
        
        }

        private void OnCompleteHandler(LoadingStepBase step)
        { 
        
        }

        private void OnFailedHandler(LoadingStepBase step, string error)
        { 
        
        }






        /*
        public struct userAttributes { };
        public struct appAttributes { };


        async private void Start()
        {
            //ConfigManager.SetEnvironmentID("test");
            //https://docs.unity3d.com/Packages/com.unity.remote-config@3.1/manual/CodeIntegration.html

            Debug.LogError("time0 " + Time.time);

            if (Utilities.CheckForInternetConnection())
            {
                await UnityServices.InitializeAsync();
            }

            RemoteConfigService.Instance.SetEnvironmentID("46d321da-5a26-4ccb-8f65-fa182534bc1b");  //5179be91-7d01-4123-99d4-4ccf784333d6
            Debug.LogError("time1 " + Time.time);
            //RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
            RemoteConfigService.Instance.FetchCompleted += FetchCompletedHandler;
            Debug.LogError("time2 " + Time.time);
        }


        private void FetchCompletedHandler(ConfigResponse config)
        {
            Debug.LogError("time3 " + Time.time);
            Debug.LogError("RemoteInt : " + RemoteConfigService.Instance.appConfig.GetInt("RemoteInt"));
        }
        */
    }
}
