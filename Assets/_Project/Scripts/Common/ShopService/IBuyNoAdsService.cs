using System;
using Zenject;

namespace GameScene.Common
{
    public interface IBuyNoAdsService : IInitializable
    {
        public event Action OnBuySuccess;
        public event Action OnBuyUnavailable;
        public event Action OnBuyFailed;
        
        public void BuyNoAds();
    }
}