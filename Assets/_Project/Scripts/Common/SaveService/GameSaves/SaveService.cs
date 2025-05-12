using Cysharp.Threading.Tasks;
using GameScene.Repositories;
using Zenject;

namespace GameScene.Common.DataSaveSystem
{
    public class SaveService : IInitializable
    {
        public GameData Data;

        private ILocalSaveService _localSaveService;
        private IGlobalSaveService _globalSaveService;

        protected SaveService(ILocalSaveService localSaveService, IGlobalSaveService globalSaveService)
        {
            _localSaveService = localSaveService;
            _globalSaveService = globalSaveService;
        }
        
        public void Initialize()
        {
            Data = new GameData();
            Load().Forget();
        }

        public void Save()
        {
            SaveTask().Forget();
        }

        private async UniTask SaveTask()
        {
            bool isSaved = await _globalSaveService.Save(Data);
            
            if (!isSaved)
                _localSaveService.Save(Data);
        }

        private async UniTask Load()
        {
            GameData globalData = await _globalSaveService.Load();
            GameData localData = _localSaveService.Load();

            if (globalData != null && localData != null && globalData.SaveTime > localData.SaveTime)
            {
                Data = globalData;
            }
            else if (localData != null)
            {
                Data = localData;
            }
        }
    }
}