using System.Collections.Generic;

namespace GOALS.Scenes
{
    public class ScenesDictionaryHolder
    {
        private readonly Dictionary<EnumSceneType, string> _scenes;


        public ScenesDictionaryHolder()
        {
            _scenes = new Dictionary<EnumSceneType, string>
            {
                { EnumSceneType.Main, "Main" },
                { EnumSceneType.MainMenu, "MainMenu" },
            };
        }

        public string GetSceneName(EnumSceneType type)
        {
            if (_scenes.ContainsKey(type))
                return _scenes[type];
            else
                return default;
        }
    }
}
