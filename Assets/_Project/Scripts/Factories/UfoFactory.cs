using System;
using System.Threading;
using GameScene.Repositories;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Entities.Player;
using GameScene.Level;
using Zenject;
using GameScene.Entities.UFOs;
using GameScene.Factories.ScriptableObjects;

namespace GameScene.Factories
{
    public class UfoFactory : Factory
    {
        private UfoFactoryData _factoryData;
        private GameStateController _gameStateController;
        private CancellationTokenSource _tokenSource;

        public PoolObjects<Ufo> PoolUfo { get; private set; }

        public UfoFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            UfoFactoryData factoryData,
            GameStateController gameStateController) : base(transformParent, spawnTransform)
        {
            _factoryData = factoryData;
            _gameStateController = gameStateController;
            _gameStateController.OnRestart += StartSpawnUFO;
            _gameStateController.OnFinish += StopSpawnUFO;
            StartSpawnUFO();
        }

        public override void Destroy()
        {
            _gameStateController.OnFinish -= StopSpawnUFO;
            _gameStateController.OnRestart -= StartSpawnUFO;
        }

        private void CreatePool()
        {
            PoolUfo = new PoolObjects<Ufo>(_factoryData.Prefab, _factoryData.SizePool, TransformParent.transform);
        }

        private async void StartSpawnUFO()
        {
            _tokenSource = new CancellationTokenSource();
            await SpawnUFOs();
        }

        private void StopSpawnUFO()
        {
            _tokenSource.Cancel();
        }

        private async UniTask SpawnUFOs()
        {
            try
            {
                while (_tokenSource.IsCancellationRequested == false);
                {
                    float time = Random.Range(_factoryData.MinTimeSpawn, _factoryData.MaxTimeSpawn);
                    await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _tokenSource.Token);
                    foreach (Ufo UFO in PoolUfo.Objects)
                    {
                        if (!UFO.gameObject.activeSelf)
                        {
                            UFO.Activate(SpawnTransform.GetPosition());
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