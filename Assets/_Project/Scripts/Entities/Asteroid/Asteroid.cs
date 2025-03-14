using UnityEngine;
using System;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid : MonoBehaviour
    {
        public event Action OnDestroyed;
        public event Action<Transform> OnDestroyed1;
        
        [SerializeField] private Rigidbody2D _rb;

        private Vector2 _velocity;

        public Asteroid Create(Transform transformSpawn, Transform transformParent, Vector2 velocity)
        {
            Asteroid asteroid = Instantiate(this, transformSpawn, transformParent);
            asteroid._velocity = velocity;
            asteroid.Deactivate();
            
            return asteroid;
        }
        
        public void Activate(Transform transformSpawn)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = transformSpawn.position;
            _rb.linearVelocity = _velocity;
        }
        
        public void Deactivate()
        {
            OnDestroyed?.Invoke();
            OnDestroyed1?.Invoke(transform);
            gameObject.SetActive(false);
        }
    }
}