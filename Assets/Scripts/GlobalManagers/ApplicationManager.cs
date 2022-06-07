using DllSky.Components.Services;
using DllSky.Services;
using GOALS.Windows;
using GOALS.Windows.ApplicationLoading;

namespace GOALS.GlobalManagers
{
    public class ApplicationManager : AutoLocatorComponent
    {
        private WindowsManager _windowsManager;




        private void Start()
        {
            _windowsManager = ComponentLocator.Resolve<WindowsManager>();

            ApplicationInitializing();
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
