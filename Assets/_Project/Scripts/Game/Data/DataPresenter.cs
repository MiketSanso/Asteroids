using Cysharp.Threading.Tasks;
using GameScene.Models;
using Zenject;

namespace GameScene.Common.DataSaveSystem
{
    public class DataPresenter : IInitializable
    {
        private DataModel _dataModel = new DataModel();
        
        private readonly ISaveService _localSaveService;
        private readonly ISaveService _globalSaveService;

        protected DataPresenter(ISaveService localSaveService, ISaveService globalSaveService)
        {
            _localSaveService = localSaveService;
            _globalSaveService = globalSaveService;
        }
        
        public void Initialize()
        {
            Load().Forget();
        }

        public void Save()
        {
            SaveTask().Forget();
        }

        public void ChangeAdsState(bool isAdsOff)
        {
            _dataModel.IsAdsOff = isAdsOff;
        }
        
        public void ChangeMaxScore(float maxScore)
        {
            _dataModel.MaxScore = maxScore;
        }
        
        public bool GetAdsState()
        {
            return _dataModel.IsAdsOff;
        }
        
        public float GetMaxScore()
        {
            return _dataModel.MaxScore;
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