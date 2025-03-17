using System;
using System.Threading;
using GameScene.Repositories;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Entities.Player;
using GameScene.Level;

namespace GameScene.Entities.UFOs
{
    public class UFOFactory : MonoBehaviour
    {
        [SerializeField] private float _inTimeSpawn;
        [SerializeField] private float _maxTimeSpawn;

        private EndPanel _endPanel;
        private PlayerUI _player;
        private PoolObjects _poolObjects;
        private CancellationTokenSource _tokenSource;

        private void OnDestroy()
        {
            _player.OnDeath -= StopSpawnUFO;
            _endPanel.OnRestart -= StartSpawnUFO;
        }

        public void Initialize(PlayerUI player, PoolObjects poolObjects, EndPanel endPanel)
        {
            _endPanel = endPanel;
            _endPanel.OnRestart += StartSpawnUFO;
            _player = player;
            _poolObjects = poolObjects;
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
                foreach (Ufo UFO in _poolObjects.Ufos)
                {
                    if (!UFO.gameObject.activeSelf)
                    {
                        UFO.Activate(_poolObjects.GetRandomTransform());
                        break;
                    }
                }
            } while (_tokenSource.IsCancellationRequested == false);
        }
    }
}