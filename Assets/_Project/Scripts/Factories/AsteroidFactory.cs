using System;
using Cysharp.Threading.Tasks;
using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Common;
using GameScene.Repositories.Configs;
using Zenject;
using GameScene.Game;
using GameSystem.Common.LoadAssetSystem;
using GameScene.Common.ConfigSaveSystem;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory<AsteroidFactoryConfig, Asteroid, AsteroidTrigger>, IInitializable, IDisposable
    {
        private const string ASTEROID_KEY = "Asteroid";
        private const string ASTEROID_SMALL_KEY = "AsteroidSmall";
        private const string SMALL_ASTEROID_CONFIG = "SmallAsteroidConfig";
        private const string ASTEROID_CONFIG = "AsteroidConfig";
        private const string FACTORY_CONFIG = "AsteroidFactoryConfig";
        
        private int _destroyed;
        private Transform _destroyedPosition;
       
        private AsteroidConfig _asteroidData;
        private AsteroidConfig _asteroidDataSmall;
        private PoolObjects<Asteroid> PoolSmallObjects;
        
        private readonly ScoreRepository _scoreRepository;
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            IAnalyticService analyticService,
            GameStateController gameStateController,
            AddressablePrefabLoader<AsteroidTrigger> addressablePrefabLoader,
            IInstantiator instantiator,
            ScoreRepository scoreRepository,
            ConfigLoadService configLoadService,
            MusicService musicService) : base(gameStateController, transformParent, spawnTransform, analyticService, addressablePrefabLoader, instantiator, configLoadService, musicService)
        {
            _scoreRepository = scoreRepository;
        }

        public async void Initialize()
        {
            GameStateController.OnResume += RestartFly;
            GameStateController.OnRestart += RestartFly;

            _asteroidData = await ConfigLoadService.Load<AsteroidConfig>(ASTEROID_CONFIG);
            _asteroidDataSmall = await ConfigLoadService.Load<AsteroidConfig>(SMALL_ASTEROID_CONFIG);
            Data = await ConfigLoadService.Load<AsteroidFactoryConfig>(FACTORY_CONFIG);
            
            PoolObjects = new PoolObjects<Asteroid>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
            
            PoolSmallObjects = new PoolObjects<Asteroid>(PreloadSmall, 
                GetSmall, 
                Return, 
                Data.SizePool * Data.CountFragments);
            
            RestartFly();
        }
        
        public void Dispose()
        {
            GameStateController.OnResume -= RestartFly;
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
            AsteroidTrigger asteroidTrigger = Instantiator.InstantiatePrefabForComponent<AsteroidTrigger>(
                await AddressablePrefabLoader.Load(ASTEROID_KEY), 
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
            AsteroidTrigger asteroidTrigger = Instantiator.InstantiatePrefabForComponent<AsteroidTrigger>(
                await AddressablePrefabLoader.Load(ASTEROID_SMALL_KEY), 
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
            MusicService.DestroyObject();
            _destroyed++;
            AnalyticService.AddDestroyedAsteroid();
            
            if (_destroyed == Data.SizePool * (1 + Data.CountFragments))
            {
                RestartFly();
            }
        }
        
        private void ActivateSmall(int scoreSize, Transform transform)
        {
            _destroyedPosition = transform;
            
            for (int i = 0; i < Data.CountFragments; i++)
            {
                PoolSmallObjects.Get();
            }
        }
    }
}