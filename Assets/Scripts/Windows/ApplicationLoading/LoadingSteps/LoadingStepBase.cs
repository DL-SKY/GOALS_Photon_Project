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

        public Action<LoadingStepBase> OnStart;
        public Action<LoadingStepBase, float> OnChangeProgress;
        public Action<LoadingStepBase> OnComplete;
        public Action<LoadingStepBase, string> OnFailed;

        protected float _progress;        


        public LoadingStepBase(EnumLoadingStep completeNextStep, EnumLoadingStep failedNextStep)
        {
            this.completeNextStep = completeNextStep;
            this.failedNextStep = failedNextStep;
        }


        public void Start()
        {
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
            OnChangeProgress?.Invoke(this, 1.0f);

            CustomComplete();
            OnComplete?.Invoke(this);

            Debug.LogError($"Complete {this.GetType()}");
        }

        protected void Failed(string error)
        {
            CustomFailed(error);
            OnFailed?.Invoke(this, error);

            Debug.LogError($"Failed {this.GetType()} by {error}");
        }


        protected abstract void CustomStart();


        protected virtual void CustomComplete() { }
        protected virtual void CustomFailed(string error) { }
    }
}
