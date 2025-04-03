using GameScene.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroyed;

        public readonly GameObject ThisObject;
        
        private readonly AsteroidData _asteroidData;
        private readonly Rigidbody2D _rb;
        public Asteroid(AsteroidData asteroidData,
        Rigidbody2D rb)
        {
            _asteroidData = asteroidData;
            _rb = rb;
            ThisObject = rb.gameObject;
        }
        
        public void Activate(Vector2 transformSpawn)
        {
            ThisObject.SetActive(true);
            ThisObject.transform.position = transformSpawn;
            
            float velocityX = Random.Range(_asteroidData.Velocity.x - _asteroidData.SprayVelocity, _asteroidData.Velocity.x + _asteroidData.SprayVelocity);
            float velocityY = Random.Range(_asteroidData.Velocity.x - _asteroidData.SprayVelocity, _asteroidData.Velocity.x + _asteroidData.SprayVelocity);
            Vector2 newVelocity = new Vector2(velocityX, velocityY);
            _rb.linearVelocity = newVelocity;
        }
        
        public void Deactivate()
        {
            ThisObject.SetActive(false);
        }
        
        public void Destroy()
        {
            Deactivate();
            OnDestroyed?.Invoke(_asteroidData.ScoreSize, ThisObject.transform);
        }
    }
}