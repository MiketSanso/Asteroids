using System;
using GameScene.Interfaces;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameScene.Entities.Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _timeDeactivate;
        [SerializeField] private Rigidbody2D _rb;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDestroyableEnemy enemy))
            {
                enemy.Destroy(false);
                Deactivate();
            }
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }
        
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public async UniTask Shot(Transform transformSpawn)
        {
            Activate();
            
            transform.position = transformSpawn.position;
                    
            float angle = (transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            _rb.linearVelocity = direction * _speed;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_timeDeactivate));
            Deactivate();
        }
    }
}