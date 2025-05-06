using UnityEngine;
using UnityEngine.Advertisements;

namespace GameScene.Infrastructure
{
    public class UnityAdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private string _gameId;
        
        private readonly string _androidGameId = "5833054";
        private readonly bool _testMode = true;

        private void Awake()
        {
            InitializeAds();
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        private void InitializeAds()
        {
            Advertisement.debugMode = _testMode;
            Advertisement.Initialize(_androidGameId, _testMode, this);
        }
    }
}