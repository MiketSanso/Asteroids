using Cysharp.Threading.Tasks;
using GameScene.Models.Configs;

namespace GameScene.Common.ConfigSaveSystem
{
    public interface IConfigLoadService
    {
        public UniTask<T> Load<T>(string key) where T : Config;
    }
}