using GameScene.Common;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidTrigger : IDestroyableEnemy
    {
        private Asteroid _asteroid;

        public override void Destroy()
        {
            _asteroid.Destroy(gameObject);
        }

        public void Initialize(Asteroid asteroid)
        {
            _asteroid = asteroid;
        }
    }
}