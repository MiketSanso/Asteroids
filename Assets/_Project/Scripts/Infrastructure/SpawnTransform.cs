using GameScene.Level.ScriptableObjects;
using UnityEngine;

namespace GameScene.Level
{
    public class SpawnTransform
    {
        private readonly SpawnPositionData _spawnPositionData;

        public SpawnTransform(SpawnPositionData spawnPositionData)
        {
            _spawnPositionData = spawnPositionData;
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