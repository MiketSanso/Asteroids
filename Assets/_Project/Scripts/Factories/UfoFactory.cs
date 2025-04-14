using System;
using GameSystem;
using System.Threading;
using GameScene.Repositories;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Level;
using GameScene.Entities.UFOs;
using GameScene.Factories.ScriptableObjects;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public class UfoFactory : Factory<UfoFactoryData, Ufo, UfoMovement>, IInitializable
    {
        private const string UfoKey = "Ufo";

        private readonly UfoData _ufoData;
        private CancellationTokenSource _tokenSource;
        private ScoreRepository _scoreRepository;
        
        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            UfoFactoryData factoryData,
            UfoData ufoData,
            GameStateController gameStateController,
            IAnalyticSystem analyticSystem,
            LoadPrefab<UfoMovement> loadPrefab,
            IInstantiator instantiator,
            ScoreRepository scoreRepository) : base(factoryData, gameStateController, transformParent, spawnTransform, analyticSystem, loadPrefab, instantiator)
        {
            _scoreRepository = scoreRepository;
            _ufoData = ufoData;
            
            PoolObjects = new PoolObjects<Ufo>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
        }
        
        public void Initialize()
        {
            GameStateController.OnCloseGame += Destroy;
            GameStateController.OnRestart += PoolObjects.ReturnAll;
            GameStateController.OnRestart += StartSpawn;
            GameStateController.OnFinish += StopSpawn;

            StartSpawn();
        }
        
        private async UniTask<Ufo> Preload()
        {
            UfoMovement ufoMovement = Instantiator.InstantiatePrefabForComponent<UfoMovement>(
                await LoadPrefab.LoadPrefabFromAddressable(UfoKey), 
                TransformParent.transform);
            Ufo ufo = new Ufo(_ufoData, ufoMovement.gameObject);
            
            ufo.OnDestroy += DestroyUfo;
            ufo.OnDestroy += _scoreRepository.AddScore;
            
            ufoMovement.Initialize(ufo, _ufoData);
            ufo.Deactivate();
            return ufo;
        }

        private void Get(Ufo ufo)
        {
            ufo.Activate(SpawnTransform.GetPosition());
        }

        private void DestroyUfo(int scoreSize, Transform transform)
        {
            AnalyticSystem.AddDestroyedUfo();
        }

        private void Destroy()
        {
            GameStateController.OnCloseGame -= Destroy;
            GameStateController.OnRestart -= PoolObjects.ReturnAll;
            GameStateController.OnFinish -= StopSpawn;
            GameStateController.OnRestart -= StartSpawn;
            
            foreach (Ufo ufo in PoolObjects.Pool)
            {
                ufo.OnDestroy -= DestroyUfo;
                ufo.OnDestroy -= _scoreRepository.AddScore;
            }
        }

        private void StartSpawn()
        {
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
