using DllSky.Components.Services;
using DllSky.Services;
using GOALS.ScriptableObjects.Application;
using GOALS.Windows;
using GOALS.Windows.ApplicationLoading;
using UnityEngine;

namespace GOALS.GlobalManagers
{
    public class ApplicationManager : AutoLocatorComponent
    {
        [SerializeField] private ApplicationConfig _config;

        private WindowsManager _windowsManager;        


        private void Start()
        {
            _windowsManager = ComponentLocator.Resolve<WindowsManager>();

            ApplicationInitializing();
        }


        public ApplicationConfig GetConfig()
        {
            return _config;
        }


        private void ApplicationInitializing()
        {
            //TODO: Path window
            _windowsManager.ShowWindow<ApplicationLoadingWindow>(ApplicationLoadingWindow.PathPrefab,
                Windows.Enums.EnumWindowLayer.Loading, 
                includeInWindowsList: false);
        }
    }
}
