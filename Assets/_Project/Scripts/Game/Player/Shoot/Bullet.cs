using System;
using Cysharp.Threading.Tasks;
using GameScene.Common;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _timeDeactivate;
        [SerializeField] private Rigidbody2D _rb;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDestroyableEnemy enemy))
            {
                enemy.Destroy();
                Deactivate();
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate(Vector2 spawnPosition)
        {
            transform.position = spawnPosition;
            gameObject.SetActive(true);
        }

        public async UniTask Shot(Transform spawnPosition)
        {
            var angle = (spawnPosition.eulerAngles.z + 90) * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            _rb.linearVelocity = direction * _speed;

            await UniTask.Delay(TimeSpan.FromSeconds(_timeDeactivate));
            Deactivate();
        }
    }
}