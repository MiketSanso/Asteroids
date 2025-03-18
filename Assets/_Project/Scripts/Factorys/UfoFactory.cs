using System;
using System.Threading;
using GameScene.Repositories;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Entities.Player;
using GameScene.Level;
using Zenject;

namespace GameScene.Entities.UFOs
{
    public class UfoFactory : MonoBehaviour
    {
        [SerializeField] private float _inTimeSpawn;
        [SerializeField] private float _maxTimeSpawn;

        [Inject] private EndPanel _endPanel;
        [Inject] private PlayerUI _player;
        private UfoPool _poolUfo;
        private CancellationTokenSource _tokenSource;

        private void OnDestroy()
        {
            _player.OnDeath -= StopSpawnUFO;
            _endPanel.OnRestart -= StartSpawnUFO;
        }

        public void Initialize(UfoPool poolUfo)
        {
            _endPanel.OnRestart += StartSpawnUFO;
            _poolUfo = poolUfo;
            _player.OnDeath += StopSpawnUFO;
            StartSpawnUFO();
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