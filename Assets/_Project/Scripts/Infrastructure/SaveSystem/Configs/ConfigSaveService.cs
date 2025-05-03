using Cysharp.Threading.Tasks;
using GameScene.Configs;

namespace _Project.Scripts.Infrastructure
{
    public abstract class ConfigSaveService
    {
        public abstract UniTask<T> Load<T>(string key) where T : Config;
    }
}