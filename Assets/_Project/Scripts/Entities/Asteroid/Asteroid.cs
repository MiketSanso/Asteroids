using _Project.Scripts.Infrastructure;
using GameScene.Configs;
using GameScene.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid : IPooledObject
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroy;

        private readonly GameObject _gameObject;
        private readonly AsteroidConfig _asteroidData;
        private readonly Rigidbody2D _rb;
        private readonly MusicService _musicService;
        
        public Asteroid(AsteroidConfig asteroidData,
        Rigidbody2D rb,
        GameObject gameObject)
        {
            _asteroidData = asteroidData;
            _rb = rb;
            _gameObject = gameObject;
        }
        
        public void Activate(Vector2 transformSpawn)
        {
            _gameObject.SetActive(true);
            _gameObject.transform.position = transformSpawn;
            
            float velocityX = Random.Range(-1 * _asteroidData.SprayVelocity, 1 * _asteroidData.SprayVelocity);
            float velocityY = Random.Range(-1 * _asteroidData.SprayVelocity, 1 * _asteroidData.SprayVelocity);
            Vector2 newVelocity = new Vector2(velocityX, velocityY);
            _rb.linearVelocity = newVelocity;
        }
        
        public void Deactivate()
        {
            _gameObject.SetActive(false);
        }
        
        public void Destroy(GameObject destroyedObject)
        {
            OnDestroy?.Invoke(_asteroidData.ScoreSize, destroyedObject.transform);
            Deactivate();
        }
    }
}