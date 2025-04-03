using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Factories.ScriptableObjects;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory, IInitializable
    {
        public readonly PoolObjects<Asteroid> PoolAsteroids;
        public readonly PoolObjects<Asteroid> PoolSmallAsteroids;
        
        private int _destroyed;
        private readonly GameStateController _gameStateController;
        private AsteroidFactoryData _asteroidFactoryData;
        private AsteroidData _asteroidData;
        private AsteroidData _asteroidDataSmall;
        private Transform _destroyedPosition;
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            IInstantiator instantiator,
            AsteroidFactoryData factoryData, 
            GameStateController gameStateController,
            AsteroidData asteroidData,
            AsteroidData asteroidDataSmall) : base(transformParent, spawnTransform, instantiator)
        {
            _asteroidData = asteroidData;
            _asteroidDataSmall = asteroidDataSmall;
            _gameStateController = gameStateController;
            _asteroidFactoryData = factoryData;

            PoolAsteroids = new PoolObjects<Asteroid>(Preload, 
                GetAction, 
                ReturnAction, 
                _asteroidFactoryData.SizePool);
            
            PoolSmallAsteroids = new PoolObjects<Asteroid>(PreloadSmall, 
                GetActionSmall, 
                ReturnAction, 
                _asteroidFactoryData.SizePool * _asteroidFactoryData.countFragments);

            RestartFly();
        }

        private Asteroid Preload()
        {
            Rigidbody2D rb = Instantiator.InstantiatePrefabForComponent<Rigidbody2D>(_asteroidFactoryData.Prefab, TransformParent.transform);
            Asteroid asteroid = new Asteroid(_asteroidData, rb);
            asteroid.Deactivate();
            return asteroid;
        }
        
        private Asteroid PreloadSmall()
        {
            Rigidbody2D rb = Instantiator.InstantiatePrefabForComponent<Rigidbody2D>(_asteroidFactoryData.Prefab, TransformParent.transform);
            Asteroid asteroid = new Asteroid(_asteroidDataSmall, rb);
            asteroid.Deactivate();
            return asteroid;
        }

        private void GetAction(Asteroid asteroid) => asteroid.Activate(SpawnTransform.GetPosition());

        private void GetActionSmall(Asteroid asteroid)
        {
            _destroyedPosition = asteroid.ThisObject.transform;
            asteroid.Activate(_destroyedPosition.position);
        }

        private void ReturnAction(Asteroid asteroid)
        {
            asteroid.Deactivate();
        }

        private void ReturnActionSmall(Asteroid asteroid) => asteroid.Deactivate();
            
        
        private void Destroy()
        {
            _gameStateController.OnRestart -= RestartFly;
            _gameStateController.OnCloseGame -= Destroy;
            
            foreach (Asteroid asteroid in PoolAsteroids.Pool)
            {
                asteroid.OnDestroyed -= ActivateSmallAsteroids;
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
            
            foreach (Asteroid asteroid in PoolSmallAsteroids.Pool)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
        }

        public void Initialize()
        {
            _gameStateController.OnRestart += RestartFly;
            _gameStateController.OnCloseGame += Destroy;
            
            foreach (Asteroid asteroid in PoolAsteroids.Pool)
            {
                asteroid.OnDestroyed += ActivateSmallAsteroids;
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
            
            foreach (Asteroid asteroid in PoolSmallAsteroids.Pool)
            {
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
        }
        
        private void RestartFly()
        {
            PoolAsteroids.ReturnAll();
            PoolSmallAsteroids.ReturnAll();
            
            for (int i = 0; i < _asteroidFactoryData.SizePool; i++)
            {
                PoolAsteroids.Get();
            }
            
            _destroyed = 0;
        }
        
        private void AddDestroyedAsteroid(int scoreSize, Transform transform)
        {
            _destroyed++;
            
            if (_destroyed == _asteroidFactoryData.SizePool * (1 + _asteroidFactoryData.countFragments))
            {
                _destroyed = 0;
                RestartFly();
            }
        }
        
        private void ActivateSmallAsteroids(int scoreSize, Transform transform)
        {
            int countActivatedAsteroids = 0;
            
            for (int i = 0; i < _asteroidFactoryData.countFragments; i++)
            {
                PoolSmallAsteroids.Get();
            }
        }
    }
}