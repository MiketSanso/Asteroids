using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        public Asteroid Asteroid { get; private set; }

        public AsteroidUI Create(Transform transformSpawn, Transform transformParent, Vector2 velocity, float sprayVelocity)
        {
            AsteroidUI asteroid = Instantiate(this, transformSpawn, transformParent);
            asteroid.Asteroid = new Asteroid(velocity, sprayVelocity);
            asteroid.Asteroid.Deactivate(gameObject, transform);
            
            return asteroid;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyDestroyer asteroidDestroyer))
            {
                Asteroid.Deactivate(gameObject, transform);
                asteroidDestroyer.Destroy();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IEnemyDestroyer asteroidDestroyer))
            {
                Asteroid.Deactivate(gameObject, transform);
                asteroidDestroyer.Destroy();
            }
        }
        
        public void Activate(Transform transformSpawn)
        {
            Asteroid.Activate(gameObject, transformSpawn, _rb);
        }
    }
}