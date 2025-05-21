using System.Runtime.CompilerServices;
using GameScene.Entities.PlayerSpace;
using UnityEngine;
using Zenject;

namespace GameScene.Game
{
    public class GamePresenter : ITickable
    {
        private GameView _gameView;
        
        private readonly Player _player;
        private readonly Laser _laser;
        
        public GamePresenter(Laser laser, Player player)
        {
            _laser = laser;
            _player = player;
        }

        public void Initialize(GameView gameView)
        {
            _gameView = gameView;
        }

        public void Tick()
        {
            if (_gameView != null)
            {
                float speed = Mathf.Round(Mathf.Abs(_player.GetComponent<Rigidbody2D>().linearVelocity.magnitude) * 100) / 100f;
            
                _gameView.UpdateUI(speed,
                    _player.transform.position,
                    Mathf.Round(_player.transform.rotation.eulerAngles.z),
                    _laser.CountShotsLaser,
                    _laser.TimeRechargeLaser);
            }
            else
            {
                Debug.LogWarning("В GamePresenter ожидание GameView...");
            }
        }
    }
}