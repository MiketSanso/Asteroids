using System;
using System.Threading;
using GameScene.Models;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Game;
using GameScene.Entities.UFOs;
using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using GameSystem.Common.LoadAssetSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public class UfoFactory : Factory<UfoFactoryConfig, Ufo>, IInitializable, IDisposable
    {
        private const string UFO_KEY = "Ufo";
        private const string FACTORY_CONFIG = "UfoFactoryConfig";
        private const string UFO_CONFIG = "UfoConfig";

        private UfoConfig _ufoConfig;
        private CancellationTokenSource _tokenSource;
        
        private readonly ScorePresenter _scorePresenter;
        
        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            GameEventBus gameEventBus,
            IAnalyticService analyticService,
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
            Data = await ConfigLoadService.Load<UfoFactoryConfig>(FACTORY_CONFIG);
            _ufoConfig = await ConfigLoadService.Load<UfoConfig>(UFO_CONFIG);
            
            PoolObjects = new PoolObjects<Ufo>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
            
            GameEventBus.OnRestart += StartSpawn;
            GameEventBus.OnResume += StartSpawn;
            GameEventBus.OnFinish += StopSpawn;
            
            StartSpawn();
        }
        
        public void Dispose()
        {
            GameEventBus.OnFinish -= StopSpawn;
            GameEventBus.OnResume -= StartSpawn;
            GameEventBus.OnRestart -= StartSpawn;
            
            foreach (Ufo ufo in PoolObjects.Pool)
            {
                ufo.OnDestroy -= DestroyUfo;
                ufo.OnDestroy -= _scorePresenter.AddScore;
            }
            
            PoolObjects.Pool.Clear();
        }
        
        private async UniTask<Ufo> Preload()
        {
            UfoMovement ufoMovement = Instantiator.InstantiatePrefabForComponent<UfoMovement>(
                await AddressablePrefabLoader.Load(UFO_KEY), 
                TransformParent.transform);

            Ufo ufo = new Ufo(_ufoConfig, ufoMovement.gameObject);
            
            ufo.OnDestroy += DestroyUfo;
            ufo.OnDestroy += _scorePresenter.AddScore;
            
            ufoMovement.Initialize(ufo, _ufoConfig);
            ufo.Deactivate();
            return ufo;
        }

        private void Get(Ufo ufo)
        {
            ufo.Activate(SpawnTransform.GetPosition());
        }

        private void DestroyUfo(int scoreSize, Transform transform)
        {
            MusicService.DestroyObject();
            AnalyticService.AddDestroyedUfo();
        }

        private void StartSpawn()
        {
            PoolObjects.ReturnAll();
            _tokenSource = new CancellationTokenSource();
            Spawn().Forget();
        }

        private void StopSpawn()
        {
            _tokenSource.Cancel();
        }

        private async UniTask Spawn()
        {
            try
            {
                while (_tokenSource.IsCancellationRequested == false)
                {
                    float time = Random.Range(Data.MinTimeSpawn, Data.MaxTimeSpawn);
                    await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _tokenSource.Token);
                    PoolObjects.Get().Forget();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation was cancelled!");
            }
        }
    } 
}
