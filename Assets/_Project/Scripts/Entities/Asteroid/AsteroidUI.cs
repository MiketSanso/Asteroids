using UnityEngine;
using GameScene.Interfaces;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour, IPooledObject, IDestroyableEnemy
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroyed;
        
        [SerializeField] private Vector2 _velocity;
        [SerializeField] private float _sprayVelocity;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private int _scoreSize;
        
        private readonly Asteroid _asteroid = new Asteroid();
        
        public void Destroy()
        {
             _asteroid.Deactivate(gameObject);
             OnDestroyed?.Invoke(_scoreSize, transform);
        }
        
        public void Activate(Vector2 positionSpawn)
        {
            _asteroid.Activate(gameObject, positionSpawn, _rb, _velocity, _sprayVelocity);
        }
        
        public void Deactivate()
        {
            _asteroid.Deactivate(gameObject);
        }
    }
}