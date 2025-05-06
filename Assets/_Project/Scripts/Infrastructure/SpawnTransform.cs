using Cysharp.Threading.Tasks;
using GameScene.Infrastructure.ConfigSaveSystem;
using GameScene.Repositories.Configs;
using UnityEngine;
using Zenject;

namespace GameScene.Infrastructure
{
    public class SpawnTransform : IInitializable
    {
        private const string SPAWN_TRANSFORM_CONFIG = "SpawnPositionConfig";
        
        private SpawnPositionConfig _spawnPositionData;
        
        private readonly ConfigSaveService _configSaveService;

        public SpawnTransform(ConfigSaveService configSaveService)
        {
            _configSaveService = configSaveService;
        }

        public async void Initialize()
        {
            _spawnPositionData = await _configSaveService.Load<SpawnPositionConfig>(SPAWN_TRANSFORM_CONFIG);
            
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