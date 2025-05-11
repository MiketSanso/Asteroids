using Cysharp.Threading.Tasks;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Repositories.Configs;
using UnityEngine;
using Zenject;

namespace GameScene.Common
{
    public class SpawnTransform : IInitializable
    {
        private const string SPAWN_TRANSFORM_CONFIG = "SpawnPositionConfig";
        
        private SpawnPositionConfig _spawnPositionData;
        
        private readonly ConfigLoadService _configLoadService;

        public SpawnTransform(ConfigLoadService configLoadService)
        {
            _configLoadService = configLoadService;
        }

        public async void Initialize()
        {
            _spawnPositionData = await _configLoadService.Load<SpawnPositionConfig>(SPAWN_TRANSFORM_CONFIG);
            
            while (_spawnPositionData == null)
            {
                await UniTask.DelayFrame(1);
            }
        }
        
        public Vector2 GetPosition()
        {
            Vector2 position;

            var isSpawnX = Random.Range(0, 2) == 1;

            if (isSpawnX)
            {
                var isRight = Random.Range(0, 2) == 1;

                if (isRight)
                    position = new Vector2(_spawnPositionData.MaxPositionX,
                        Random.Range(_spawnPositionData.MinPositionY, _spawnPositionData.MaxPositionY));
                else
                    position = new Vector2(-_spawnPositionData.MaxPositionX,
                        Random.Range(_spawnPositionData.MinPositionY, _spawnPositionData.MaxPositionY));
            }
            else
            {
                var isUp = Random.Range(0, 2) == 1;

                if (isUp)
                    position = new Vector2(
                        Random.Range(_spawnPositionData.MinPositionX, _spawnPositionData.MaxPositionX),
                        _spawnPositionData.MaxPositionY);
                else
                    position = new Vector2(
                        Random.Range(_spawnPositionData.MinPositionX, _spawnPositionData.MaxPositionX),
                        -_spawnPositionData.MaxPositionY);
            }

            return position;
        }
    }
}