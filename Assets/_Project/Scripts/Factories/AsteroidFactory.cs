using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Factories.ScriptableObjects;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory<AsteroidFactoryData, Asteroid>, IInitializable
    {
        public readonly PoolObjects<Asteroid> PoolSmallObjects;
        
        private int _destroyed;
        private readonly AsteroidData _asteroidData;
        private readonly AsteroidData _asteroidDataSmall;
        private Transform _destroyedPosition;
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            IInstantiator instantiator,
            AsteroidFactoryData factoryData, 
            GameStateController gameStateController,
            AsteroidData asteroidData,
            AsteroidData asteroidDataSmall) : base(factoryData, gameStateController, transformParent, spawnTransform, instantiator)
        {
            _asteroidData = asteroidData;
            _asteroidDataSmall = asteroidDataSmall;
            
            PoolObjects = new PoolObjects<Asteroid>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
            
            PoolSmallObjects = new PoolObjects<Asteroid>(PreloadSmall, 
                GetSmall, 
                Return, 
                Data.SizePool * Data.countFragments);
        }
        
        public void Initialize()
        {
            GameStateController.OnRestart += RestartFly;
            GameStateController.OnCloseGame += Destroy;
            
            RestartFly();
        }

        private Asteroid Preload()
        {
            AsteroidTrigger asteroidTrigger = Instantiator.InstantiatePrefabForComponent<AsteroidTrigger>(Data.Prefab, TransformParent.transform);
            Rigidbody2D rb = asteroidTrigger.GetComponent<Rigidbody2D>();
            Asteroid asteroid = new Asteroid(_asteroidData, rb, asteroidTrigger.gameObject);
            
            asteroid.OnDestroyed += AddDestroyedAsteroid;
            asteroid.OnDestroyed += ActivateSmall;
            
            asteroidTrigger.Initialize(asteroid);
            asteroid.Deactivate();
            return asteroid;
        }
        
        private void Get(Asteroid asteroid) => asteroid.Activate(SpawnTransform.GetPosition());
        
        private Asteroid PreloadSmall()
        {
            AsteroidTrigger asteroidTrigger = Instantiator.InstantiatePrefabForComponent<AsteroidTrigger>(Data.SmallPrefab, TransformParent.transform);
            Rigidbody2D rb = asteroidTrigger.GetComponent<Rigidbody2D>();
            Asteroid asteroid = new Asteroid(_asteroidDataSmall, rb, asteroidTrigger.gameObject);
            
            asteroid.OnDestroyed += AddDestroyedAsteroid;
            
            asteroidTrigger.Initialize(asteroid);
            asteroid.Deactivate();
            return asteroid;
        }
        
        private void GetSmall(Asteroid asteroid) => asteroid.Activate(_destroyedPosition.position);
        
        private void Destroy()
        {
            GameStateController.OnRestart -= RestartFly;
            GameStateController.OnCloseGame -= Destroy;
            
            foreach (Asteroid asteroid in PoolObjects.Pool)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
                asteroid.OnDestroyed -= ActivateSmall;
            }
            
            foreach (Asteroid asteroid in PoolSmallObjects.Pool)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
        }
        
        private void RestartFly()
        {
            PoolObjects.ReturnAll();
            PoolSmallObjects.ReturnAll();
            
            for (int i = 0; i < Data.SizePool; i++)
            {
                PoolObjects.Get();
            }

            _destroyed = 0;
        }
        
        private void AddDestroyedAsteroid(int scoreSize, Transform transform)
        {
            _destroyed++;
            
            if (_destroyed == Data.SizePool * (1 + Data.countFragments))
            {
                RestartFly();
            }
        }
        
        private void ActivateSmall(int scoreSize, Transform transform)
        {
            _destroyedPosition = transform;
            
            for (int i = 0; i < Data.countFragments; i++)
            {
                PoolSmallObjects.Get();
            }
        }
    }
}