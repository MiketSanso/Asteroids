using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class InterstitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string INTERSTITIAL_AD_UNIT_ID = "Interstitial_Android";
        
        [SerializeField] private Button _button;
        
        private IUnityAdsLoadListener _unityAdsLoadListenerImplementation;

        private void Start()
        {
            _button.onClick.AddListener(ShowInterstitialAd);
            Advertisement.Load(INTERSTITIAL_AD_UNIT_ID, this);
        }
        
        public void ShowInterstitialAd()
        {
            Advertisement.Show(INTERSTITIAL_AD_UNIT_ID, this);
        }
        
        void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void OnUnityAdsAdLoaded(string placementId) { }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
        
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
    }
}