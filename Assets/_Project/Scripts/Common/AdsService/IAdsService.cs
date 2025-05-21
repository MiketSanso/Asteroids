using Zenject;

namespace GameScene.Common
{
    public interface IAdsService : IInitializable
    {
        public void ShowInterstitialAds();

        public void ShowRewardedAds();
    }
}