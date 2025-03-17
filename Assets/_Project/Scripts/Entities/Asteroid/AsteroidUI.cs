using UnityEngine;
using System;
using GameScene.Interfaces;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour, IDestroyableEnemy
    {
        public event Action OnDestroyed;
        public event Action<int> OnDestroyedWithScore;
        public event Action<Transform> OnDestroyedWithPoint;
        
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private int scoreSize;
        
        private Asteroid _asteroid;

        public AsteroidUI Create(Transform transformSpawn, 
            Transform transformParent, 
            Vector2 velocity, 
            float sprayVelocity)
        {
            AsteroidUI asteroid = Instantiate(this, transformSpawn.position, Quaternion.identity, transformParent);
            asteroid._asteroid = new Asteroid(velocity, sprayVelocity);
            asteroid._asteroid.Deactivate(gameObject);
            
            return asteroid;
        }

        public void Destroy(bool isPlayer)
        {
             _asteroid.Deactivate(gameObject);

             if (!isPlayer)
             {
                 OnDestroyedWithScore?.Invoke(scoreSize);
                 OnDestroyedWithPoint?.Invoke(transform);
                 OnDestroyed?.Invoke();
             }
        }
        
        public void Activate(Transform transformSpawn)
        {
            _asteroid.Activate(gameObject, transformSpawn, _rb);
        }
        
        public void Deactivate()
        {
            _asteroid.Deactivate(gameObject);
        }
    }
}