using System;
using GameSystem;
using System.Threading;
using _Project.Scripts.Infrastructure;
using GameScene.Repositories;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Level;
using GameScene.Entities.UFOs;
using GameScene.Configs;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public class UfoFactory : Factory<UfoFactoryConfig, Ufo, UfoMovement>, IInitializable, IDisposable
    {
        private const string UFO_KEY = "Ufo";
        private const string FACTORY_CONFIG = "UfoFactoryConfig";
        private const string UFO_CONFIG = "UfoConfig";

        private UfoConfig _ufoConfig;
        private CancellationTokenSource _tokenSource;
        
        private readonly ScoreRepository _scoreRepository;
        
        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            GameStateController gameStateController,
            IAnalyticService analyticService,
            LoadPrefab<UfoMovement> loadPrefab,
            IInstantiator instantiator,
            ScoreRepository scoreRepository,
            ConfigSaveService configSaveService,
            MusicService musicService) : base(gameStateController, transformParent, spawnTransform, analyticService, loadPrefab, instantiator, configSaveService, musicService)
        {
            _scoreRepository = scoreRepository;
        }
        
        public async void Initialize()
        {
            Data = await ConfigSaveService.Load<UfoFactoryConfig>(FACTORY_CONFIG);
            _ufoConfig = await ConfigSaveService.Load<UfoConfig>(UFO_CONFIG);
            
            PoolObjects = new PoolObjects<Ufo>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
            
            GameStateController.OnRestart += StartSpawn;
            GameStateController.OnFinish += StopSpawn;
            
            StartSpawn();
        }
        
        public void Dispose()
        {
            GameStateController.OnFinish -= StopSpawn;
            GameStateController.OnRestart -= StartSpawn;
            
            foreach (Ufo ufo in PoolObjects.Pool)
            {
                ufo.OnDestroy -= DestroyUfo;
                ufo.OnDestroy -= _scoreRepository.AddScore;
            }
            
            PoolObjects.Pool.Clear();
        }
        
        private async UniTask<Ufo> Preload()
        {
            UfoMovement ufoMovement = Instantiator.InstantiatePrefabForComponent<UfoMovement>(
                await LoadPrefab.LoadPrefabFromAddressable(UFO_KEY), 
                TransformParent.transform);
            Ufo ufo = new Ufo(_ufoConfig, ufoMovement.gameObject);
            
            ufo.OnDestroy += DestroyUfo;
            ufo.OnDestroy += _scoreRepository.AddScore;
            
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

        public void StartSpawn()
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
                    PoolObjects.Get();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation was cancelled!");
            }
        }
    } 
}
