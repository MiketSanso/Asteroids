using Zenject;

namespace GameScene.Interfaces
{
    public interface IAnalyticSystem : IInitializable
    {
        public void AddBulletShot();
        public void AddDestroyedAsteroid();
        public void AddDestroyedUfo();
        public void StartGame();
        public void EndGame();
        public void UseLaser();
    }
}