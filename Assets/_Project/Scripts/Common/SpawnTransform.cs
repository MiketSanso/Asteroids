using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using UnityEngine;
using Zenject;

namespace GameScene.Common
{
    public class SpawnTransform : IInitializable
    {
        private const string SPAWN_TRANSFORM_CONFIG = "SpawnPositionConfig";
        
        private SpawnPositionConfig _spawnPositionData;
        
        private readonly IConfigLoadService _configLoadService;

        public SpawnTransform(IConfigLoadService configLoadService)
        {
            _configLoadService = configLoadService;
        }

        public async void Initialize()
        {
            _spawnPositionData = await _configLoadService.Load<SpawnPositionConfig>(SPAWN_TRANSFORM_CONFIG);
        }
        
        public Vector2 GetPosition()
        {
            Vector2 position;

            var isSpawnX = Random.value > .5f;

            if (isSpawnX)
            {
                var isRight = Random.value > .5f;

                if (isRight)
                    position = new Vector2(_spawnPositionData.MaxPositionX,
                        Random.Range(_spawnPositionData.MinPositionY, _spawnPositionData.MaxPositionY));
                else
                    position = new Vector2(-_spawnPositionData.MaxPositionX,
                        Random.Range(_spawnPositionData.MinPositionY, _spawnPositionData.MaxPositionY));
            }
            else
            {
                var isUp = Random.value > .5f;

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