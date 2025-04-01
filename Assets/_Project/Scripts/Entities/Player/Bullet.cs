using System;
using GameScene.Interfaces;
using UnityEngine;
using Cysharp.Threading.Tasks;
using GameScene.Level;

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
        
        public async void Activate(Transform spawnPosition)
        {
            transform.position = spawnPosition.position;
            gameObject.SetActive(true);
            await Shot(spawnPosition);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private async UniTask Shot(Transform spawnPosition)
        {
            float angle = (spawnPosition.eulerAngles.z + 90) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            _rb.linearVelocity = direction * _speed;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_timeDeactivate));
            Deactivate();
        }
    }
}