using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Factories.ScriptableObjects;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory
    {
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        private int _destroyed;
        private int _countAsteroids;
        private readonly GameStateController _gameStateController;
        private readonly AsteroidFactoryData _factoryData;

        public PoolObjects<AsteroidUI> PoolAsteroids { get; private set; }
        public PoolObjects<AsteroidUI> PoolSmallAsteroids { get; private set; }
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            AsteroidFactoryData factoryData, 
            GameStateController gameStateController)
        {
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            _gameStateController = gameStateController;
            _factoryData = factoryData;
            _gameStateController.OnRestart += RestartFly;
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
        
        public void Destroy()
        {
            _gameStateController.OnRestart -= RestartFly;
            
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
        
        private void CreatePool()
        {
            PoolAsteroids = new PoolObjects<AsteroidUI>(_factoryData.Prefab, _factoryData.SizePool, TransformParent.transform);
            
            PoolSmallAsteroids = new PoolObjects<AsteroidUI>(_factoryData.SmallPrefab, _factoryData.SizePool, TransformParent.transform);
        }
        
        private void RestartFly()
        {
            _destroyed = 0;

            PoolAsteroids.DeactivateObjects();
            
            foreach (AsteroidUI asteroid in PoolAsteroids.Objects)
            {
                asteroid.Activate(SpawnTransform.GetPosition());
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