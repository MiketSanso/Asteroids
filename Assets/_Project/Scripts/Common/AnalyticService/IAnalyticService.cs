using System;
using Zenject;

namespace GameScene.Common
{
    public interface IAnalyticService : IInitializable, IDisposable
    {
        public void AddBulletShot();
        
        public void AddDestroyedAsteroid();
        
        public void AddDestroyedUfo();
        
        public void UseLaser();
    }
}