using Zenject;

namespace GameScene.Common
{
    public interface IInterstitialAdsService : IInitializable
    {
        public void ShowAds();
    }
}