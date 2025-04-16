using Zenject;

namespace GameScene.Interfaces
{
    public interface IAnalyticService : IInitializable
    {
        public void AddBulletShot();
        
        public void AddDestroyedAsteroid();
        
        public void AddDestroyedUfo();
        
        public void UseLaser();
    }
}