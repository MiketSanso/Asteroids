using UnityEngine;
using System;
using GameScene.Interfaces;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour, IDestroyableEnemy
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroyed;
        
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private int _scoreSize;
        
        private Asteroid _asteroid;

        public void Initialize(Asteroid asteroid)
        {
            _asteroid = asteroid;
        }
        
        public void Destroy()
        {
             _asteroid.Deactivate(gameObject);
             OnDestroyed?.Invoke(_scoreSize, transform);
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