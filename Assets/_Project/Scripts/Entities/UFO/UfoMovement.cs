using GameScene.Entities.Player;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.UFOs
{
    public class UfoMovement : MonoBehaviour,  IDestroyableEnemy
    {
        private Ufo _ufo;
        private UfoData _ufoData;
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

                transform.position += direction * _ufoData.Speed * Time.deltaTime;
            }
        }

        public void Initialize(Ufo ufo, UfoData ufoData)
        {
            _ufoData = ufoData;
            _ufo = ufo;
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