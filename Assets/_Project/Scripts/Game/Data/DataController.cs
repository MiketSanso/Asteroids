using Cysharp.Threading.Tasks;
using GameScene.Common.DataSaveSystem;
using Zenject;

namespace GameScene.Models
{
    public class DataController : IInitializable
    {
        private DataModel _dataModel = new DataModel();
        
        private readonly ISaveService _localSaveService;
        private readonly ISaveService _globalSaveService;
        
        public DataController(ISaveService localSaveService, ISaveService globalSaveService)
        {
            _localSaveService = localSaveService;
            _globalSaveService = globalSaveService;
        }

        public void Initialize()
        {
            Load().Forget(); 
        }
        
        public float GetMaxScore() => _dataModel.MaxScore;
        public bool CanShowAds() => _dataModel.IsAdsOn;

        public async void SetMaxScore(float score)
        {
            _dataModel.MaxScore = score;
            await SaveTask(); 
        }

        public async void SetAdsState(bool isOn)
        {
            _dataModel.IsAdsOn = isOn;
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