using Zenject;
using System;

namespace GameScene.Common
{
    public interface IAdsService : IInitializable, IDisposable
    {
        public void ShowInterstitialAds();

        public void ShowRewardedAds();
    }
}