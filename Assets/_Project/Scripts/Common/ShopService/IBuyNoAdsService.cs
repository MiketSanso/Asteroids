using System;
using Zenject;

namespace GameScene.Common
{
    public interface IBuyNoAdsService : IInitializable
    {
        public event Action OnDisableAds;
        public event Action<string> OnSendInfo;
        
        public void BuyNoAds();
    }
}