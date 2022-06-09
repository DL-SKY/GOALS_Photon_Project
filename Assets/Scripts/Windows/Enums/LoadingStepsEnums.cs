namespace GOALS.Windows.Enums
{
    public enum EnumLoadingStep
    {
        NA = 0,

        InitializeUnityService,

        LoadRemoteConfig,
        ApplyRemoteConfig,

        LoadLocalConfig,
        ApplyLocalConfig,

        PhotonConnect,

        LastStep = 999,
    }
}
