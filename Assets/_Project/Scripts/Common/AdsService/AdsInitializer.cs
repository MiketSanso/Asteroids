using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace GameScene.Common
{
    public class UnityAdsInitializer : IInitializable, IUnityAdsInitializationListener
    {
        private IUnityAdsInitializationListener _unityAdsInitializationListenerImplementation;
        private const string ADS_ID = "5833054";
        
        public void Initialize()
        {
            InitializeAds().Forget();
        }

        private async UniTask InitializeAds()
        {
            await UnityServices.InitializeAsync();

            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize(ADS_ID, testMode: true, this);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Initialize Ads is complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Initialize Ads is failed.");
        }
    }
}