using UnityEngine;
using System;
using GameScene.Interfaces;
using GameScene.Level;
using Zenject;

namespace GameScene.Entities.Player
{
    public class PlayerUI : MonoBehaviour
    {
        public event Action OnDeath;
        
        private EndPanel _endPanel;

        private void Start()
        {
            _endPanel.OnRestart += Activate;
        }
        
        private void OnDestroy()
        {
            _endPanel.OnRestart -= Activate;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDestroyableEnemy enemy))
            {
                enemy.Destroy(true);
                Deactivate();
            }
        }
        
        [Inject]
        public void Construct(EndPanel endPanel)
        {
            _endPanel = endPanel;
        }
        
        private void Deactivate()
        {
            OnDeath?.Invoke();
            gameObject.SetActive(false);
        }

        private void Activate()
        {
            gameObject.SetActive(true);
            transform.position = Vector3.zero;
        }
    }
}