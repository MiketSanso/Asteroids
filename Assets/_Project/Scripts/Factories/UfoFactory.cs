using System;
using System.Threading;
using GameScene.Repositories;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Entities.Player;
using GameScene.Level;
using Zenject;
using GameScene.Entities.UFOs;

namespace GameScene.Factories
{
    public class UfoFactory : Factory
    {
        private float _inTimeSpawn;
        private float _maxTimeSpawn;
        private PlayerUI _playerUI;
        private UfoPool _poolUfo;
        private EndPanel _endPanel;
        private CancellationTokenSource _tokenSource;
        
        [Inject]
        public void Construct(PlayerUI playerUI, EndPanel endPanel)
        {
            _endPanel = endPanel;
            _playerUI = playerUI;
        }

        public void Initialize(UfoPool poolUfo)
        {
            _endPanel.OnRestart += StartSpawnUFO;
            _poolUfo = poolUfo;
            _playerUI.OnDeath += StopSpawnUFO;
            StartSpawnUFO();
        }
        
        public override void Destroy()
        {
            _playerUI.OnDeath -= StopSpawnUFO;
            _endPanel.OnRestart -= StartSpawnUFO;
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
            do
            {
                float time = Random.Range(_inTimeSpawn, _maxTimeSpawn);
                await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _tokenSource.Token);
                foreach (Ufo UFO in _poolUfo.Ufos)
                {
                    if (!UFO.gameObject.activeSelf)
                    {
                        UFO.Activate(_poolUfo.GetRandomTransform());
                        break;
                    }
                }
            } while (_tokenSource.IsCancellationRequested == false);
        }
    }
}