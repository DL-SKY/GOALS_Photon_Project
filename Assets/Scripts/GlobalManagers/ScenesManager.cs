using DllSky.Components.Services;
using GOALS.Scenes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GOALS.GlobalManagers
{
    public class ScenesManager : AutoLocatorComponent
    {
        public event Action<AsyncOperation> OnAsyncLoadUpdated;
        public event Action<AsyncOperation> OnAsyncLoadCompleted;

        private ScenesDictionaryHolder _scenesDic = new ScenesDictionaryHolder();
        private AsyncOperation _asyncOperation;


        private void Update()
        {
            if (_asyncOperation != null)
            {
                OnAsyncLoadUpdated?.Invoke(_asyncOperation);
            }
        }


        public AsyncOperation LoadSceneAsync(EnumSceneType type, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            _asyncOperation = SceneManager.LoadSceneAsync(_scenesDic.GetSceneName(type), loadMode);
            _asyncOperation.completed += OnCompletedHandler;
            
            return _asyncOperation;
        }


        private void OnCompletedHandler(AsyncOperation operation)
        {
            _asyncOperation.completed -= OnCompletedHandler;
            OnAsyncLoadCompleted?.Invoke(operation);

            _asyncOperation = null;
        }
    }
}
