using UnityEngine.Advertisements;

namespace GameScene.Common
{
    public class UnityRewardedAds : IUnityAdsLoadListener, IUnityAdsShowListener, IRewardedAdsService
    {
        private const string ANDROID_AD_UNIT_ID = "Rewarded_Android";
        
        private string _adUnitId;
        private bool _isAdsReady;
        
        private readonly GameStateController _gameStateController;
        
        private UnityRewardedAds(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }
        
        public void Initialize()
        {
            _adUnitId = ANDROID_AD_UNIT_ID;
            _gameStateController.OnRestart += LoadAds;
    
            Advertisement.Load(_adUnitId, this);
        }
        
        public void Dispose()
        {
            _gameStateController.OnRestart -= LoadAds;
        }
        
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (adUnitId.Equals(_adUnitId))
            {
                _isAdsReady = true;
            }
        }
    
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    
        public void OnUnityAdsShowStart(string placementId) { }
    
        public void OnUnityAdsShowClick(string placementId) { }
    
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            _gameStateController.ResumeGame();
        }
        
        public void ShowAds()
        {
            if (_isAdsReady)
            {
                _isAdsReady = false;
                Advertisement.Show(_adUnitId, this);
            }
        }
    
        private void LoadAds()
        {
            Advertisement.Load(_adUnitId, this);
        }
    }
}

