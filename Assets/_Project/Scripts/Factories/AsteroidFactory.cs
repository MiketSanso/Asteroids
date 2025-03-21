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
        private readonly AsteroidFactoryData _factoryData;
        private int _destroyed;
        private int _countAsteroids; 
        private readonly EndPanel _endPanel;

        public PoolObjects<AsteroidUI> PoolAsteroids { get; private set; }
        public PoolObjects<AsteroidUI> PoolSmallAsteroids { get; private set; }

        [Inject]
        public AsteroidFactory(AsteroidFactoryData factoryData, EndPanel endPanel)
        {
            _endPanel = endPanel;
            _factoryData = factoryData;
            _endPanel.OnRestart += RestartFly;
            CreatePool();
            RestartFly();

            foreach (AsteroidUI asteroid in PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed += ActivateSmallAsteroids;
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in PoolSmallAsteroids.Objects)
            {
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
        }
        
        public override void Destroy()
        {
            _endPanel.OnRestart -= RestartFly;
            
            foreach (AsteroidUI asteroid in PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed -= ActivateSmallAsteroids;
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in PoolSmallAsteroids.Objects)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
        }
        
        protected override void CreatePool()
        {
            PoolAsteroids = new PoolObjects<AsteroidUI>(_factoryData.Prefab, 
                _factoryData.SizePool, 
                _factoryData.TransformParent.Transform);
            
            PoolSmallAsteroids = new PoolObjects<AsteroidUI>(_factoryData.SmallPrefab, 
                _factoryData.SizePool, 
                _factoryData.TransformParent.Transform);
        }
        
        private void RestartFly()
        {
            _destroyed = 0;

            PoolAsteroids.DeactivateObjects();
            
            foreach (AsteroidUI asteroid in PoolAsteroids.Objects)
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
            
            foreach (AsteroidUI asteroid in PoolSmallAsteroids.Objects)
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