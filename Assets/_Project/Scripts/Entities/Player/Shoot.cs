using _Project.Scripts.Entities;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform _transformSpawn;
        [SerializeField] private float _speed;
        [SerializeField] private int _timeDestroy;
        
        private PoolObjects _poolObjects;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnBullet();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }

        public void Initialize(PoolObjects poolObjects)
        {
            _poolObjects = poolObjects;
        }

        private void SpawnBullet()
        {
            foreach (Bullet bullet in _poolObjects.Bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.position = bullet.SpawnPosition;
                    
                    float angle = (transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
                    Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    if (bullet.TryGetComponent(out Rigidbody2D rigidbody))
                        rigidbody.linearVelocity = direction * _speed;
                    else
                    {
                        Rigidbody2D rb = bullet.gameObject.AddComponent<Rigidbody2D>();
                        rb.gravityScale = 0;
                        rb.linearVelocity = direction * _speed;
                    }
                }
            }
        }
    }
}