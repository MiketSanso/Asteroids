using System;
using Cysharp.Threading.Tasks;
using GameSystem;
using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Factories.ScriptableObjects;
using GameScene.Interfaces;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory<AsteroidFactoryData, Asteroid, AsteroidTrigger>, IInitializable, IDisposable
    {
        private const string ASTEROID_KEY = "Asteroid";
        private const string ASTEROID_SMALL_KEY = "AsteroidSmall";
        
        public readonly PoolObjects<Asteroid> PoolSmallObjects;
        
        private int _destroyed;
        private Transform _destroyedPosition;
        
        private readonly AsteroidData _asteroidData;
        private readonly AsteroidData _asteroidDataSmall;
        private readonly ScoreRepository _scoreRepository;
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            IAnalyticService analyticService,
            AsteroidFactoryData factoryData, 
            GameStateController gameStateController,
            AsteroidData asteroidData,
            AsteroidData asteroidDataSmall,
            LoadPrefab<AsteroidTrigger> loadPrefab,
            IInstantiator instantiator,
            ScoreRepository scoreRepository) : base(factoryData, gameStateController, transformParent, spawnTransform, analyticService, loadPrefab, instantiator)
        {
            _asteroidData = asteroidData;
            _asteroidDataSmall = asteroidDataSmall;
            _scoreRepository = scoreRepository;
            
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
            
            RestartFly();
        }
        
        public void Dispose()
        {
            Debug.Log("Отписка");
            GameStateController.OnRestart -= RestartFly;
            
            foreach (Asteroid asteroid in PoolObjects.Pool)
            {
                asteroid.OnDestroy -= AddDestroyAsteroid;
                asteroid.OnDestroy -= ActivateSmall;
                asteroid.OnDestroy -= _scoreRepository.AddScore;
            }
            
            foreach (Asteroid asteroid in PoolSmallObjects.Pool)
            {
                asteroid.OnDestroy -= AddDestroyAsteroid;
                asteroid.OnDestroy -= _scoreRepository.AddScore;
            }
            
            PoolObjects.Pool.Clear();
        }
        
        public void RestartFly()
        {
            PoolObjects.ReturnAll();
            PoolSmallObjects.ReturnAll();
            
            for (int i = 0; i < Data.SizePool; i++)
            {
                PoolObjects.Get();
            }

            _destroyed = 0;
        }

        private async UniTask<Asteroid> Preload()
        {
            Debug.Log("Подписка Большой Шар");

            AsteroidTrigger asteroidTrigger = Instantiator.InstantiatePrefabForComponent<AsteroidTrigger>(
                await LoadPrefab.LoadPrefabFromAddressable(ASTEROID_KEY), 
                TransformParent.transform);
            Rigidbody2D rb = asteroidTrigger.GetComponent<Rigidbody2D>();
            Asteroid asteroid = new Asteroid(_asteroidData, rb, asteroidTrigger.gameObject);
            
            asteroid.OnDestroy += AddDestroyAsteroid;
            asteroid.OnDestroy += ActivateSmall;
            asteroid.OnDestroy += _scoreRepository.AddScore;
            
            asteroidTrigger.Initialize(asteroid);
            asteroid.Deactivate();
            return asteroid;
        }
        
        private void Get(Asteroid asteroid) => asteroid.Activate(SpawnTransform.GetPosition());
        
        private async UniTask<Asteroid> PreloadSmall()
        {
            Debug.Log("Подписка Малый Шар");
            
            AsteroidTrigger asteroidTrigger = Instantiator.InstantiatePrefabForComponent<AsteroidTrigger>(
                await LoadPrefab.LoadPrefabFromAddressable(ASTEROID_SMALL_KEY), 
                TransformParent.transform);            
            
            Rigidbody2D rb = asteroidTrigger.GetComponent<Rigidbody2D>();
            Asteroid asteroid = new Asteroid(_asteroidDataSmall, rb, asteroidTrigger.gameObject);
            
            asteroid.OnDestroy += AddDestroyAsteroid;
            asteroid.OnDestroy += _scoreRepository.AddScore;
            
            asteroidTrigger.Initialize(asteroid);
            asteroid.Deactivate();
            return asteroid;
        }
        
        private void GetSmall(Asteroid asteroid) => asteroid.Activate(_destroyedPosition.position);
        
        private void AddDestroyAsteroid(int scoreSize, Transform transform)
        {
            _destroyed++;
            AnalyticService.AddDestroyedAsteroid();
            
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