using GameScene.Models.Configs;
using GameScene.Entities.PlayerSpace;
using GameScene.Common;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.UFOs
{
    public class UfoMovement : IDestroyableEnemy
    {
        private Ufo _ufo;
        private Player _player;
        private UfoConfig _ufoConfig;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        private void Update()
        {
            if (_player.gameObject.activeSelf && _ufoConfig != null)
            {
                Vector3 direction = _player.transform.position - transform.position;
                direction.Normalize();

                transform.position += direction * _ufoConfig.Speed * Time.deltaTime;
            }
        }

        public void Initialize(Ufo ufo, UfoConfig ufoConfig)
        {
            _ufo = ufo;
            _ufoConfig = ufoConfig;
        }

        public override void Destroy()
        {
            if (_ufo != null)
                _ufo.Destroy();
            else
                Debug.LogError("У UfoMovement отсутствует комонент Ufo!");
        }
    }
}