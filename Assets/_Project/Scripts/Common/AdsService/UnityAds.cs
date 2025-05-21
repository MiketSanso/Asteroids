using System;
using GameScene.Models;
using UnityEngine.Advertisements;

namespace GameScene.Common
{
    public class UnityAds : IUnityAdsLoadListener, IUnityAdsShowListener, IAdsService, IGameEndEventer
    {
        private const string INTERSTITIAL_AD_UNIT_ID = "Interstitial_Android";
        private const string REWARDED_AD_UNIT_ID = "Rewarded_Android";

        public event Action OnRestart;
        public event Action OnResume;
        
        private IUnityAdsLoadListener _unityAdsLoadListenerImplementation;
        private bool _isAdsReady;
        
        private readonly DataController _dataController;
        
        private UnityAds(DataController dataController)
        {
            _dataController = dataController;
        }

        public void Initialize()
        {
            Advertisement.Load(INTERSTITIAL_AD_UNIT_ID, this);
            Advertisement.Load(REWARDED_AD_UNIT_ID, this);
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
                OnResume?.Invoke();
            }
            else if (placementId == INTERSTITIAL_AD_UNIT_ID)
            {
                OnRestart?.Invoke();
            }
        }
        
        public void ShowInterstitialAds()
        {
            if (!_dataController.CanShowAds())
            {
                Advertisement.Show(INTERSTITIAL_AD_UNIT_ID, this);
            }
            else
            {
                Advertisement.Load(REWARDED_AD_UNIT_ID, this);
                OnRestart?.Invoke();
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
        
    }
}