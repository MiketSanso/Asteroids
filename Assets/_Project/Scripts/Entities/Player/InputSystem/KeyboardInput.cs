using System;
using GameScene.Factories;
using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class KeyboardInput : IInputSystem
    {
        public event Action OnShotBullet;
        
        private readonly MovementController _movement;
        private readonly BulletFactory _bulletFactory;
        private readonly Laser _laser;

        public KeyboardInput(BulletFactory bulletFactory, Laser laser)
        {
            _bulletFactory = bulletFactory;
            _laser = laser;
            _movement = new MovementController();
        }

        public void Shot(Transform transformSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bulletFactory.Respawn();
                OnShotBullet?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _laser.Shot(transformSpawn);
                _laser.StartRecharge();
            }
        }
        
        public void Move(PlayerMovement playerMovement)
        {
            if (Input.GetButton("Horizontal"))
            {
                _movement.Rotate(playerMovement);
            }

            if (Input.GetKey(KeyCode.W))
            {
                _movement.Move(playerMovement);
            }
        }
    }
}