using GameScene.Common.DataSaveSystem;
using UnityEngine.Advertisements;
using Zenject;

namespace GameScene.Common
{
    public class UnityInterstitialAds : IUnityAdsLoadListener, IUnityAdsShowListener, IInitializable, IInterstitialAdsService
    {
        private const string INTERSTITIAL_AD_UNIT_ID = "Interstitial_Android";
        
        private IUnityAdsLoadListener _unityAdsLoadListenerImplementation;
        private GameStateController _gameStateController;
        private SaveService _saveService;
        
        private UnityInterstitialAds(GameStateController gameStateController, SaveService saveService)
        {
            _gameStateController = gameStateController;
            _saveService = saveService;
        }

        public void Initialize()
        {
            Advertisement.Load(INTERSTITIAL_AD_UNIT_ID, this);
        }

        public void OnUnityAdsAdLoaded(string placementId) { }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
        
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            ShowComplete();
        }
        
        public void ShowAds()
        {
            if (!_saveService.Data.IsAdsOff)
            {
                Advertisement.Show(INTERSTITIAL_AD_UNIT_ID, this);
            }
            else
            {
                ShowComplete();
            }
        }
        
        private void ShowComplete()
        {
            _gameStateController.RestartGame();
        }
    }
}