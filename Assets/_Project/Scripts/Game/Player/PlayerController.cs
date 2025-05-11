using GameScene.Common;
using GameScene.Repositories.Configs;
using GameScene.Factories;
using GameScene.Common.ConfigSaveSystem;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerController
    {
        private const string PLAYER_MOVEMENT_CONFIG = "PlayerMovementConfig";
        
        private PlayerMovementConfig _playerMovementConfig;
        private PlayerUI _playerObject;
        private Rigidbody2D _rb;
        private Transform _pointShot;
        
        private readonly ConfigLoadService _configLoadService;
        private readonly IInputService _inputService;
        private readonly BulletFactory _bulletFactory;
        private readonly Laser _laser;

        public PlayerController(BulletFactory bulletFactory, 
            Laser laser,
            ConfigLoadService configLoadService, 
            IInputService inputService)
        {
            _configLoadService = configLoadService;
            _bulletFactory = bulletFactory;
            _laser = laser;
            _inputService = inputService;
        }

        public async void Initialize(Rigidbody2D rb, 
            PlayerUI playerObject,
            Transform pointShoot)
        {
            _rb = rb;
            _playerObject = playerObject;
            _pointShot = pointShoot;
            
            _playerMovementConfig = await _configLoadService.Load<PlayerMovementConfig>(PLAYER_MOVEMENT_CONFIG);

            _inputService.OnCanShotLaser += ShotLaser;
            _inputService.OnCanShotLaser += CheckLaserShot;
            _inputService.OnCanMove += Move;
            _inputService.OnCanShotBullet += ShotBullet;
            _inputService.OnCanRotate += Rotate;
        }

        public void Destroy()
        {
            _inputService.OnCanShotLaser -= ShotLaser;
            _inputService.OnCanShotLaser -= CheckLaserShot;
            _inputService.OnCanMove -= Move;
            _inputService.OnCanShotBullet -= ShotBullet;
            _inputService.OnCanRotate -= Rotate;
        }
        
        private void CheckLaserShot()
        {
            _laser.StartRecharge();
        }
        
        private void ShotBullet()
        {
            _bulletFactory.Respawn();
        }
        
        private void ShotLaser()
        {
            _laser.Shot(_pointShot);
        }
        
        private void Rotate()
        {
            _playerObject.transform.Rotate(0, 0, _playerMovementConfig.SpeedRotation * -Input.GetAxis("Horizontal"));
        }
        
        private void Move()
        {
            float angle = (_playerObject.transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            _rb.AddForce(direction.normalized * _playerMovementConfig.SpeedMove);
            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _playerMovementConfig.MaxSpeed);
        }
    }
}