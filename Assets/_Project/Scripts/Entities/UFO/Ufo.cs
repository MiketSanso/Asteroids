using GameScene.Interfaces;
using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Entities.UFOs
{
    public class Ufo : MonoBehaviour, IPooledObject, IDestroyableEnemy
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroyed;
        
        [SerializeField] private float _speed;
        [SerializeField] private int _scoreSize;
        
        private PlayerUI _playerUI;

        [Inject]
        private void Construct(PlayerUI playerUI)
        {
            _playerUI = playerUI;
        }
        
        private void Update()
        {
            if (_playerUI.gameObject.activeSelf)
            {
                Vector3 direction = _playerUI.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * _speed * Time.deltaTime;
            }
        }

        public void Destroy()
        {
             Deactivate();
             OnDestroyed?.Invoke(_scoreSize, transform);
        }

        public void Activate(Transform transformSpawn)
        {
            transform.position = transformSpawn.position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
