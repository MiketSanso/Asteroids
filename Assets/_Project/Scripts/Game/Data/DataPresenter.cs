namespace GameScene.Models
{
    public class DataPresenter
    {
        private readonly DataService _dataService;

        public DataPresenter(DataService dataService)
        {
            _dataService = dataService;
        }
        
        public float GetMaxScore() => _dataService.MaxScore;
        public bool CanShowAds() => !_dataService.IsAdsOff;
    }
}