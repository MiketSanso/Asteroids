using UnityEngine;
using System;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid : MonoBehaviour
    {
        public event Action OnDestroyed;
        
        [SerializeField] private Rigidbody2D _rb;
        
        private Asteroid[] _smallAsteroids;

        public Asteroid Create(Transform transformSpawn, Transform transformParent, Asteroid[] smallAsteroids)
        {
            Asteroid asteroid = Instantiate(this, transformSpawn, transformParent);
            asteroid.Deactivate();
            _smallAsteroids = smallAsteroids;
            
            return asteroid;
        }
        
        public void Activate(Vector2 velocity)
        {
            gameObject.SetActive(true);
            _rb.linearVelocity = velocity;

        }
        
        private void Deactivate()
        {
            if (_smallAsteroids != null)
            {
                foreach (Asteroid asteroid in _smallAsteroids.Asteroids)
                {
                    asteroid.Activate();
                }
            }
            
            OnDestroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}