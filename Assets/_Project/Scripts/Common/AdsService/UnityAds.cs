using GameScene.Models;
using UnityEngine.Advertisements;

namespace GameScene.Common
{
    public class UnityAds : IUnityAdsLoadListener, IUnityAdsShowListener, IAdsService
    {
        private const string INTERSTITIAL_AD_UNIT_ID = "Interstitial_Android";
        private const string REWARDED_AD_UNIT_ID = "Rewarded_Android";
        
        private IUnityAdsLoadListener _unityAdsLoadListenerImplementation;
        private bool _isAdsReady;
        
        private readonly GameEventBus _gameEventBus;
        private readonly DataPresenter _dataPresenter;
        
        private UnityAds(GameEventBus gameEventBus, DataPresenter dataPresenter)
        {
            _gameEventBus = gameEventBus;
            _dataPresenter = dataPresenter;

            _gameEventBus.OnRestart += LoadAds;
        }

        public void Initialize()
        {
            Advertisement.Load(INTERSTITIAL_AD_UNIT_ID, this);
            Advertisement.Load(REWARDED_AD_UNIT_ID, this);
        }
        
        public void Dispose()
        {
            _gameEventBus.OnRestart -= LoadAds;
        }
        
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (adUnitId.Equals(REWARDED_AD_UNIT_ID))
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
            if (placementId == REWARDED_AD_UNIT_ID)
            {
                _gameEventBus.ResumeGame();
            }
            else if (placementId == INTERSTITIAL_AD_UNIT_ID)
            {
                ShowComplete();
            }
        }
        
        public void ShowInterstitialAds()
        {
            if (_dataPresenter.CanShowAds())
            {
                Advertisement.Show(INTERSTITIAL_AD_UNIT_ID, this);
            }
            else
            {
                ShowComplete();
            }
        }
        
        public void ShowRewardedAds()
        {
            if (_isAdsReady)
            {
                _isAdsReady = false;
                Advertisement.Show(REWARDED_AD_UNIT_ID, this);
            }
        }
        
        private void ShowComplete()
        {
            _gameEventBus.RestartGame();
        }
        
    
        private void LoadAds()
        {
            Advertisement.Load(REWARDED_AD_UNIT_ID, this);
        }
    }
}