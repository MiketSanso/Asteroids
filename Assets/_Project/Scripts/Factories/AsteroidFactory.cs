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
        public readonly PoolObjects<AsteroidUI> PoolAsteroids;
        public readonly PoolObjects<AsteroidUI> PoolSmallAsteroids;
        
        private int _destroyed;
        private readonly int _countAsteroids;
        private readonly GameStateController _gameStateController;
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            AsteroidFactoryData factoryData, 
            GameStateController gameStateController,
            IInstantiator instantiator) : base(transformParent, spawnTransform)
        {
            _gameStateController = gameStateController;
            _countAsteroids = factoryData.SizePool;

            PoolAsteroids = new PoolObjects<AsteroidUI>(factoryData.Prefab, factoryData.SizePool, TransformParent.transform, instantiator);
            PoolSmallAsteroids = new PoolObjects<AsteroidUI>(factoryData.SmallPrefab, factoryData.SizePool, TransformParent.transform, instantiator);

            Initialize();
            RestartFly();
        }
        
        public override void Destroy()
        {
            _gameStateController.OnRestart -= RestartFly;
            _gameStateController.OnCloseGame -= Destroy;
            
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

        private void Initialize()
        {
            _gameStateController.OnRestart += RestartFly;
            _gameStateController.OnCloseGame += Destroy;
            
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
        
        private void RestartFly()
        {
            PoolAsteroids.DeactivateObjects();
            PoolSmallAsteroids.DeactivateObjects();
            
            foreach (AsteroidUI asteroid in PoolAsteroids.Objects)
            {
                asteroid.Activate(SpawnTransform.GetPosition());
            }
            
            _destroyed = 0;
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