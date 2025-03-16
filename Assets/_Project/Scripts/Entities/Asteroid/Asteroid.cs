using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid
    {
        public event Action OnDestroyed;
        public event Action<Transform> OnDestroyed1;

        private readonly Vector2 _velocity;
        private readonly float _sprayVelocity;

        public Asteroid(Vector2 velocity, float sprayVelocity)
        {
            _velocity = velocity;
            _sprayVelocity = sprayVelocity;
        }
        
        public void Activate(GameObject thisObject, Transform transformSpawn, Rigidbody2D rb)
        {
            thisObject.SetActive(true);
            thisObject.transform.position = transformSpawn.position;
            
            float velocityX = Random.Range(_velocity.x - _sprayVelocity, _velocity.x + _sprayVelocity);
            float velocityY = Random.Range(_velocity.x - _sprayVelocity, _velocity.x + _sprayVelocity);
            Vector2 velocity = new Vector2(velocityX, velocityY);
            rb.linearVelocity = velocity;
        }
        
        public void Deactivate(GameObject thisObject, Transform thisTransform)
        {
            OnDestroyed?.Invoke();
            OnDestroyed1?.Invoke(thisTransform);

            thisObject.SetActive(false);
        }
    }
}