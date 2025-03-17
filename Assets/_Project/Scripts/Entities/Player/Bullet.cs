using System;
using GameScene.Interfaces;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameScene.Entities.Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _timeDeactivate;

        public Bullet Create(PlayerUI player, Transform transformParent)
        {
            Bullet bullet = Instantiate(this, player.transform.position, Quaternion.identity, transformParent);
            return bullet;
        }
        
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

        public async UniTask DelayedDeactivate()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_timeDeactivate));
            Deactivate();
        }
    }
}