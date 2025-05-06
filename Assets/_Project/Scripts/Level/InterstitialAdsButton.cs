using GameScene.Infrastructure;
using GameScene.Infrastructure.DataSaveSystem;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Zenject;

namespace GameScene.Level
{
    public class InterstitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string INTERSTITIAL_AD_UNIT_ID = "Interstitial_Android";
        
        [SerializeField] private Button _button;
        [SerializeField] private EndPanel _endPanel;
        
        private IUnityAdsLoadListener _unityAdsLoadListenerImplementation;
        private GameStateController _gameStateController;
        private SaveService _saveService;
        
        [Inject]
        private void Construct(GameStateController gameStateController, SaveService saveService)
        {
            _gameStateController = gameStateController;
            _saveService = saveService;
        }

        private void Start()
        {
            _button.onClick.AddListener(ShowInterstitialAd);
            Advertisement.Load(INTERSTITIAL_AD_UNIT_ID, this);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ShowInterstitialAd);
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
        
        private void ShowInterstitialAd()
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