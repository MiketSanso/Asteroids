using Cysharp.Threading.Tasks;
using GameScene.Repositories.Configs;

namespace GameScene.Infrastructure.ConfigSaveSystem
{
    public abstract class ConfigSaveService
    {
        public abstract UniTask<T> Load<T>(string key) where T : Config;
    }
}