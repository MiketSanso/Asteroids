using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Factories.ScriptableObjects;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory
    {
        private AsteroidFactoryData _factoryData;
        private int _destroyed;
        private int _countAsteroids; 
        private EndPanel _endPanel;

        public AsteroidsPool PoolAsteroids { get; private set; }

        public AsteroidFactory(AsteroidFactoryData factoryData)
        {
            _factoryData = factoryData;
            _endPanel.OnRestart += RestartFly;
            RestartFly();

            foreach (AsteroidUI asteroid in PoolAsteroids.Asteroids)
            {
                asteroid.OnDestroyed += ActivateSmallAsteroids;
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in PoolAsteroids.SmallAsteroids)
            {
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
        }
        
        [Inject]
        public virtual void Construct(EndPanel endPanel)
        {
            _endPanel = endPanel;
        }
        
        public override void Destroy()
        {
            _endPanel.OnRestart -= RestartFly;
            
            foreach (AsteroidUI asteroid in PoolAsteroids.Asteroids)
            {
                asteroid.OnDestroyed -= ActivateSmallAsteroids;
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in PoolAsteroids.SmallAsteroids)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
        }
        
        protected override void CreatePool()
        {
            PoolAsteroids = new AsteroidsPool(_factoryData.SmallAsteroidData, 
                _factoryData.AsteroidData, 
                _factoryData.SizePool, 
                _factoryData.TransformParent.Transform);
        }
        
        private void RestartFly()
        {
            _destroyed = 0;

            PoolAsteroids.DeactivateObjects();
            
            foreach (AsteroidUI asteroid in PoolAsteroids.Asteroids)
            {
                asteroid.Activate(PoolAsteroids.GetRandomTransform());
            }
        }
        
        private void AddDestroyedAsteroid(int scoreSize, Transform transform)
        {
            _destroyed++;

            if (_destroyed == _countAsteroids * 3)
            {
                _destroyed = 0;
                RestartFly();
            }
        }
        
        private void ActivateSmallAsteroids(int scoreSize, Transform transform)
        {
            int countActivatedAsteroids = 0;
            
            foreach (AsteroidUI asteroid in PoolAsteroids.SmallAsteroids)
            {
                if (countActivatedAsteroids < 2 && !asteroid.gameObject.activeSelf)
                {
                    countActivatedAsteroids++;
                    asteroid.Activate(transform.position);
                }
                else if (countActivatedAsteroids == 2)
                {
                    break;
                }
            }
        }
    }
}