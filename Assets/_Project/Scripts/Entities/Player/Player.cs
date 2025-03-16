using UnityEngine;
using System;
using GameScene.Entities.Asteroid;

namespace GameScene.Entities.Player
{
    public class Player : MonoBehaviour, IEnemyDestroyer
    {
        public event Action OnDeath;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out UFO ufo))
            {
                Destroy();
            }
        }
        
        public void Destroy()
        {
            OnDeath?.Invoke();
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            transform.position = Vector3.zero;
        }
    }
}