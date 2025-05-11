using Zenject;
using System;

namespace GameScene.Common
{
    public interface IRewardedAdsService : IInitializable, IDisposable
    {
        public void ShowAds();
    }
}