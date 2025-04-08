using System;
using System.Linq;
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
    public class UfoFactory : Factory, IInitializable
    {
        public PoolObjects<Ufo> PoolUfo;
        
        private readonly UfoFactoryData _factoryData;
        private readonly UfoData _ufoData;
        private readonly GameStateController _gameStateController;
        private CancellationTokenSource _tokenSource;
        
        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            UfoFactoryData factoryData,
            UfoData ufoData,
            GameStateController gameStateController,
            IInstantiator instantiator) : base(transformParent, spawnTransform, instantiator)
        {
            _ufoData = ufoData;
            _factoryData = factoryData;
            _gameStateController = gameStateController;
            
            PoolUfo = new PoolObjects<Ufo>(Preload, 
                GetAction, 
                ReturnAction, 
                _factoryData.SizePool);
        }

        private void Destroy()
        {
            _gameStateController.OnCloseGame -= Destroy;
            _gameStateController.OnRestart -= PoolUfo.ReturnAll;
            _gameStateController.OnFinish -= StopSpawn;
            _gameStateController.OnRestart -= StartSpawn;
        }

        public void Initialize()
        {
            _gameStateController.OnCloseGame += Destroy;
            _gameStateController.OnRestart += PoolUfo.ReturnAll;
            _gameStateController.OnRestart += StartSpawn;
            _gameStateController.OnFinish += StopSpawn;

            StartSpawn();
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
                    float time = Random.Range(_factoryData.MinTimeSpawn, _factoryData.MaxTimeSpawn);
                    await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _tokenSource.Token);
                    PoolUfo.Get();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Operation was cancelled!");
            }
        }
        
                
        public async void Spawn(Transform positionSpawn)
        {
            PoolUfo.Get();
        }
        
        private Ufo Preload()
        {
            UfoUI ufoUi = Instantiator.InstantiatePrefabForComponent<UfoUI>(_factoryData.Prefab, TransformParent.transform);
            Ufo ufo = new Ufo(_ufoData, ufoUi.gameObject);
            ufoUi.Initialize(ufo, _ufoData);
            ufo.Deactivate();
            return ufo;
        }

        private void GetAction(Ufo ufo) => ufo.Activate(SpawnTransform.GetPosition());

        private void ReturnAction(Ufo ufo) => ufo.Deactivate();
    } 
}
