using UnityEngine;

namespace GameScene.Level
{
    public class SpawnTransform : MonoBehaviour
    {
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _maxPositionY;

        public Vector2 GetPosition()
        {
            Vector2 position;
            
            bool isSpawnX = Random.Range(0, 2) == 1;
            
            if (isSpawnX)
            {
                bool isRight = Random.Range(0, 2) == 1;

                if (isRight)
                    position = new Vector2(_maxPositionX, Random.Range(_maxPositionY, -_maxPositionY));
                else
                    position = new Vector2(-_maxPositionX, Random.Range(_maxPositionY, -_maxPositionY));
            }
            else
            {
                bool isUp = Random.Range(0, 2) == 1;
                
                if (isUp)
                    position = new Vector2(Random.Range(_maxPositionX, -_maxPositionX), _maxPositionY);
                else
                    position = new Vector2(Random.Range(_maxPositionX, -_maxPositionX), -_maxPositionY);
            }

            return position;
        }
    }
}