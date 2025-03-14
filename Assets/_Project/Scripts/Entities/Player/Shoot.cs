using System;
using Cysharp.Threading.Tasks;
using GameScene.Entities.Asteroid;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private AsteroidDestroyer _prefab;
        [SerializeField] private Transform _transformSpawn;
        [SerializeField] private float _speed;
        [SerializeField] private int _timeDestroy;

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

        private async void SpawnBullet()
        {
            AsteroidDestroyer bullet = Instantiate(_prefab, _transformSpawn.position, Quaternion.identity);

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

            await DestroyBullet(bullet.gameObject);
        }

        private async UniTask DestroyBullet(GameObject bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_timeDestroy));
            Destroy(bullet);
        }
    }
}