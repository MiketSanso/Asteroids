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
        [SerializeField] private int _scoreSize;
        
        private Asteroid _asteroid;

        public void Initialize(Asteroid asteroid)
        {
            _asteroid = asteroid;
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