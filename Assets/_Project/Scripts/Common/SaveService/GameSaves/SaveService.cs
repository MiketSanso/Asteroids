using Cysharp.Threading.Tasks;
using GameScene.Interfaces;
using GameScene.Repositories;
using Zenject;

namespace GameScene.Common.DataSaveSystem
{
    public abstract class SaveService : IInitializable
    {
        public GameData Data;

        protected readonly ILocalSaveService LocalSaveService;

        protected SaveService(ILocalSaveService localSaveService)
        {
            LocalSaveService = localSaveService;
        }
        
        public void Initialize()
        {
            Data = new GameData();
            Load();
        }
        
        public abstract UniTask Save();
        
        protected abstract UniTask Load();
    }
}