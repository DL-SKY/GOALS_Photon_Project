using UnityEngine;

namespace GOALS.ScriptableObjects.Application
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Application/ApplicationConfig")]
    public class ApplicationConfig : ScriptableObject
    {
        public string remoteConfigEnvironmentID;
    }
}
