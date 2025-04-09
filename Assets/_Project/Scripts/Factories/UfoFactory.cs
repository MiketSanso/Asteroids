using System;
using System.Threading;
using GameScene.Repositories;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Level;
using GameScene.Entities.UFOs;
using GameScene.Factories.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public class UfoFactory : Factory<UfoFactoryData, Ufo>, IInitializable
    {
        private readonly UfoData _ufoData;
        private CancellationTokenSource _tokenSource;
        
        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            UfoFactoryData factoryData,
            UfoData ufoData,
            GameStateController gameStateController,
            IInstantiator instantiator) : base(factoryData, gameStateController, transformParent, spawnTransform, instantiator)
        {
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
        
        private Ufo Preload()
        {
            UfoMovement ufoMovement = Instantiator.InstantiatePrefabForComponent<UfoMovement>(Data.Prefab, TransformParent.transform);
            Ufo ufo = new Ufo(_ufoData, ufoMovement.gameObject);
            ufoMovement.Initialize(ufo, _ufoData);
            ufo.Deactivate();
            return ufo;
        }

        private void Get(Ufo ufo) => ufo.Activate(SpawnTransform.GetPosition());

        private void Destroy()
        {
            GameStateController.OnCloseGame -= Destroy;
            GameStateController.OnRestart -= PoolObjects.ReturnAll;
            GameStateController.OnFinish -= StopSpawn;
            GameStateController.OnRestart -= StartSpawn;
        }

        private async void StartSpawn()
        {
            _tokenSource = new CancellationTokenSource();
            await Spawn();
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
