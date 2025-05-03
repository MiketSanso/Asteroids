using GameScene.Configs;
using GameScene.Entities.Player;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.UFOs
{
    public class UfoMovement : MonoBehaviour,  IDestroyableEnemy
    {
        private Ufo _ufo;
        private PlayerUI _playerUI;
        private UfoConfig _ufoConfig;

        [Inject]
        private void Construct(PlayerUI playerUI)
        {
            _playerUI = playerUI;
        }
        
        private void Update()
        {
            if (_playerUI.gameObject.activeSelf && _ufoConfig != null)
            {
                Vector3 direction = _playerUI.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * _ufoConfig.Speed * Time.deltaTime;
            }
        }

        public void Initialize(Ufo ufo, UfoConfig ufoConfig)
        {
            _ufo = ufo;
            _ufoConfig = ufoConfig;
        }

        public void Destroy()
        {
            if (_ufo != null)
                _ufo.Destroy();
            else
                Debug.LogError("У UfoMovement отсутствует комонент Ufo!");
        }
    }
}