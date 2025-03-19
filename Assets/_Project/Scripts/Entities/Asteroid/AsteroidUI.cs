using UnityEngine;
using System;
using GameScene.Interfaces;
using UnityEngine.Serialization;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour, IDestroyableEnemy
    {
        public event Action OnDestroyed;
        public event Action<int> OnDestroyedWithScore;
        public event Action<Transform> OnDestroyedWithPoint;
        
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private int _scoreSize;
        
        private Asteroid _asteroid;

        public AsteroidUI Create(Vector2 positionSpawn, 
            Transform transformParent, 
            Vector2 velocity, 
            float sprayVelocity)
        {
            AsteroidUI asteroid = Instantiate(this, positionSpawn, Quaternion.identity, transformParent);
            asteroid._asteroid = new Asteroid(velocity, sprayVelocity);
            asteroid._asteroid.Deactivate(gameObject);
            
            return asteroid;
        }

        public void Destroy(bool isPlayer)
        {
             _asteroid.Deactivate(gameObject);

             if (!isPlayer)
             {
                 OnDestroyedWithScore?.Invoke(_scoreSize);
                 OnDestroyedWithPoint?.Invoke(transform);
                 OnDestroyed?.Invoke();
             }
        }
        
        public void Activate(Vector2 positionSpawn)
        {
            _asteroid.Activate(gameObject, positionSpawn, _rb);
        }
        
        public void Deactivate()
        {
            _asteroid.Deactivate(gameObject);
        }
    }
}