using GOALS.Windows.Enums;
using System;
using UnityEngine;

namespace GOALS.Windows.ApplicationLoading.LoadingSteps
{
    public abstract class LoadingStepBase
    {
        public abstract EnumLoadingStep Step { get; }

        public EnumLoadingStep completeNextStep { get; private set; }
        public EnumLoadingStep failedNextStep { get; private set; }

        public event Action<LoadingStepBase> OnStart;
        public event Action<LoadingStepBase, float> OnChangeProgress;
        public event Action<LoadingStepBase> OnComplete;
        public event Action<LoadingStepBase, string> OnFailed;

        protected float _progress;        


        public LoadingStepBase(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep)
        {
            this.completeNextStep = completeNextStep;
            this.failedNextStep = failedNextStep;
        }


        public void Start()
        {
            Debug.LogError($"Start {this.GetType()}");

            OnStart?.Invoke(this);
            CustomStart();            
        }

        public float GetProgress()
        {
            return _progress;
        }


        protected void ChangeProgress(float progress)
        {
            _progress = progress;
            OnChangeProgress?.Invoke(this, progress);

            Debug.LogError($"ChangeProgress {this.GetType()}, progress {_progress}");

            if (_progress >= 1.0f)
                Complete();
        }

        protected void Complete()
        {
            Debug.LogError($"Complete {this.GetType()}");

            OnChangeProgress?.Invoke(this, 1.0f);

            CustomComplete();
            OnComplete?.Invoke(this);            
        }

        protected void Failed(string error)
        {
            Debug.LogError($"Failed {this.GetType()} by {error}");

            OnChangeProgress?.Invoke(this, 1.0f);

            CustomFailed(error);
            OnFailed?.Invoke(this, error);            
        }


        protected abstract void CustomStart();


        protected virtual void CustomComplete() { }
        protected virtual void CustomFailed(string error) { }
    }
}
