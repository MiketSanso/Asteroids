using Cysharp.Threading.Tasks;
using GameScene.Common.DataSaveSystem;
using GameScene.Models;

namespace _Project.Scripts.Game.Data
{
    public class DataService
    {
        private DataModel _dataModel = new DataModel();
        private readonly ISaveService _localSaveService;
        private readonly ISaveService _globalSaveService;

        public DataService(ISaveService localSaveService, ISaveService globalSaveService)
        {
            _localSaveService = localSaveService;
            _globalSaveService = globalSaveService;
            Load().Forget();
        }

        public float MaxScore => _dataModel.MaxScore;
        public bool IsAdsOff => _dataModel.IsAdsOff;

        public async void SetMaxScore(float score)
        {
            _dataModel.MaxScore = score;
            await SaveTask(); 
        }

        public async void SetAdsOff(bool isOff)
        {
            _dataModel.IsAdsOff = isOff;
            await SaveTask();
        }
        
        public async void Save()
        {
            await SaveTask();
        }

        private async UniTask SaveTask() 
        {
            bool isSaved = await _globalSaveService.Save(_dataModel);
            
            if (!isSaved)
                _localSaveService.Save(_dataModel).Forget(); 
        }
        
        private async UniTask Load() 
        {             
            DataModel globalData = await _globalSaveService.Load();
            DataModel localData = await _localSaveService.Load();

            if (globalData != null && localData != null && globalData.SaveTime > localData.SaveTime)
            {
                _dataModel = globalData;
            }
            else if (localData != null)
            {
                _dataModel = localData;
            } 
        }
    }
}