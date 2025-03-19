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
        private PlayerUI _playerUI;
        private UfoPool _poolUfo;
        private EndPanel _endPanel;
        private CancellationTokenSource _tokenSource;
        
        public UfoFactory(UfoFactoryData factoryData)
        {
            _factoryData = factoryData;
            _endPanel.OnRestart += StartSpawnUFO;
            _playerUI.OnDeath += StopSpawnUFO;
            StartSpawnUFO();
        }
        
        [Inject]
        public void Construct(PlayerUI playerUI, EndPanel endPanel)
        {
            _endPanel = endPanel;
            _playerUI = playerUI;
        }

        public override void Destroy()
        {
            _playerUI.OnDeath -= StopSpawnUFO;
            _endPanel.OnRestart -= StartSpawnUFO;
        }

        protected override void CreatePool()
        {
            _poolUfo = new UfoPool(_factoryData.Prefab, _factoryData.SizePool, _factoryData.TransformParent);
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
                    foreach (Ufo UFO in _poolUfo.Ufos)
                    {
                        if (!UFO.gameObject.activeSelf)
                        {
                            UFO.Activate(_poolUfo.GetRandomTransform());
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