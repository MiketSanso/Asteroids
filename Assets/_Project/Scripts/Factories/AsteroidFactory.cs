using System;
using Cysharp.Threading.Tasks;
using GameScene.Models;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Common;
using GameScene.Models.Configs;
using Zenject;
using GameScene.Game;
using GameSystem.Common.LoadAssetSystem;
using GameScene.Common.ConfigSaveSystem;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory<AsteroidFactoryConfig, Asteroid>, IInitializable, IDisposable
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
        private PoolObjects<Asteroid> _poolSmallObjects;
        
        private readonly ScorePresenter _scorePresenter;
        
        public AsteroidFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform, 
            IAnalyticService analyticService,
            GameEventBus gameEventBus,
            AddressablePrefabLoader<GameObject> addressablePrefabLoader,
            IInstantiator instantiator,
            ScorePresenter scorePresenter,
            IConfigLoadService configLoadService,
            MusicService musicService) : base(gameEventBus, transformParent, spawnTransform, analyticService, addressablePrefabLoader, instantiator, configLoadService, musicService)
        {
            _scorePresenter = scorePresenter;
        }

        public async void Initialize()
        {
            GameEventBus.OnResume += RestartFly;
            GameEventBus.OnRestart += RestartFly;

            _asteroidData = await ConfigLoadService.Load<AsteroidConfig>(ASTEROID_CONFIG);
            _asteroidDataSmall = await ConfigLoadService.Load<AsteroidConfig>(SMALL_ASTEROID_CONFIG);
            Data = await ConfigLoadService.Load<AsteroidFactoryConfig>(FACTORY_CONFIG);
            
            PoolObjects = new PoolObjects<Asteroid>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
            
            _poolSmallObjects = new PoolObjects<Asteroid>(PreloadSmall, 
                GetSmall, 
                Return, 
                Data.SizePool * Data.CountFragments);
            
            RestartFly();
        }
        
        public void Dispose()
        {
            GameEventBus.OnResume -= RestartFly;
            GameEventBus.OnRestart -= RestartFly;
            
            foreach (Asteroid asteroid in PoolObjects.Pool)
            {
                asteroid.OnDestroy -= AddDestroyAsteroid;
                asteroid.OnDestroy -= ActivateSmall;
                asteroid.OnDestroy -= _scorePresenter.AddScore;
            }
            
            foreach (Asteroid asteroid in _poolSmallObjects.Pool)
            {
                asteroid.OnDestroy -= AddDestroyAsteroid;
                asteroid.OnDestroy -= _scorePresenter.AddScore;
            }
            
            PoolObjects.Pool.Clear();
        }
        
        private void RestartFly()
        {
            PoolObjects.ReturnAll();
            _poolSmallObjects.ReturnAll();
            
            for (int i = 0; i < Data.SizePool; i++)
            {
                PoolObjects.Get().Forget();
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
            asteroid.OnDestroy += _scorePresenter.AddScore;
            
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
            asteroid.OnDestroy += _scorePresenter.AddScore;
            
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
        
        private async void ActivateSmall(int scoreSize, Transform transform)
        {
            _destroyedPosition = transform;
            
            for (int i = 0; i < Data.CountFragments; i++)
            {
                await _poolSmallObjects.Get();
            }
        }
    }
}