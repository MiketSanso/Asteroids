using Cysharp.Threading.Tasks;
using GameScene.Repositories;

namespace GameScene.Common.DataSaveSystem
{
    public interface IGlobalSaveService
    {
        public UniTask<bool> Save(GameData gameData);
        
        public UniTask<GameData> Load();
    }
}