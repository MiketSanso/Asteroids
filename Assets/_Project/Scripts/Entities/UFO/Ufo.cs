using GameScene.Interfaces;
using UnityEngine;
using GameScene.Entities.Player;
using System;
using Zenject;

namespace GameScene.Entities.UFOs
{
    public class Ufo : MonoBehaviour, IDestroyableEnemy
    {
        public event Action<int> OnDestroyed;
        
        [SerializeField] private float _speed;
        [SerializeField] private int _scoreSize;
        
        private PlayerUI _playerUI;

        private void Update()
        {
            if (_playerUI != null)
            {
                Vector3 direction = _playerUI.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * _speed * Time.deltaTime;
            }
        }
        
        [Inject]
        public void Construct(PlayerUI playerUI)
        {
            _playerUI = playerUI;
        }

        public void Destroy(bool isPlayer)
        {
             Deactivate();
            
             if (!isPlayer) 
                 OnDestroyed?.Invoke(_scoreSize);
        }

        public void Activate(Vector2 positionSpawn)
        {
            transform.position = positionSpawn;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
