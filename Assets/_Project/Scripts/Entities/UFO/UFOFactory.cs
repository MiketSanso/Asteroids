using System;
using System.Threading;
using _Project.Scripts.Entities;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;
using GameScene.Entities.Player;

namespace GameScene.Entities.UFO
{
    public class UFOFactory : MonoBehaviour
    {
        [SerializeField] private float _inTimeSpawn;
        [SerializeField] private float _maxTimeSpawn;

        private Player _player;
        private PoolObjects _poolObjects;
        private CancellationTokenSource _tokenSource;

        private void OnDestroy()
        {
            _player.OnDeath -= StopSpawnUFO;
        }

        public void Initialize(Player player, PoolObjects poolObjects)
        {
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
                foreach (UFO UFO in _poolObjects.Ufos)
                {
                    if (!UFO.gameObject.activeSelf)
                    {
                        UFO.Activate();
                        break;
                    }
                }
            } while (_tokenSource.IsCancellationRequested == false);
        }
    }
}