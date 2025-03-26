using System;
using System.Threading;
using GameScene.Repositories;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Level;
using GameScene.Entities.UFOs;
using GameScene.Factories.ScriptableObjects;
using Zenject;

namespace GameScene.Factories
{
    public class UfoFactory : Factory
    {
        public readonly PoolObjects<Ufo> PoolUfo;
        
        private readonly UfoFactoryData _factoryData;
        private readonly GameStateController _gameStateController;
        private CancellationTokenSource _tokenSource;
        
        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            UfoFactoryData factoryData,
            GameStateController gameStateController,
            IInstantiator instantiator) : base(transformParent, spawnTransform)
        {
            _factoryData = factoryData;
            _gameStateController = gameStateController;
            PoolUfo = new PoolObjects<Ufo>(_factoryData.Prefab, _factoryData.SizePool, TransformParent.transform, instantiator);

            Initialize();
            StartSpawnUfo();
        }

        public override void Destroy()
        {
            _gameStateController.OnCloseGame -= Destroy;
            _gameStateController.OnRestart -= PoolUfo.DeactivateObjects;
            _gameStateController.OnFinish -= StopSpawnUfo;
            _gameStateController.OnRestart -= StartSpawnUfo;
        }

        private void Initialize()
        {
            _gameStateController.OnCloseGame += Destroy;
            _gameStateController.OnRestart += PoolUfo.DeactivateObjects;
            _gameStateController.OnRestart += StartSpawnUfo;
            _gameStateController.OnFinish += StopSpawnUfo;
        }

        private async void StartSpawnUfo()
        {
            _tokenSource = new CancellationTokenSource();
            await SpawnUfos();
        }

        private void StopSpawnUfo()
        {
            _tokenSource.Cancel();
        }

        private async UniTask SpawnUfos()
        {
            try
            {
                while (_tokenSource.IsCancellationRequested == false)
                {
                    float time = Random.Range(_factoryData.MinTimeSpawn, _factoryData.MaxTimeSpawn);
                    await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _tokenSource.Token);
                    foreach (Ufo ufo in PoolUfo.Objects)
                    {
                        if (!ufo.gameObject.activeSelf)
                        {
                            ufo.Activate(SpawnTransform.GetPosition());
                            break;
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            { }
        }
    }
}