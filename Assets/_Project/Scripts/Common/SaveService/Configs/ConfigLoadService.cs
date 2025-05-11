using Cysharp.Threading.Tasks;
using GameScene.Repositories.Configs;

namespace GameScene.Common.ConfigSaveSystem
{
    public abstract class ConfigLoadService
    {
        public abstract UniTask<T> Load<T>(string key) where T : Config;
    }
}