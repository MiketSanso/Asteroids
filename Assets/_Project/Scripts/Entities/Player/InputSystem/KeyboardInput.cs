using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.Player
{
    public class KeyboardInput : IInputSystem
    {
        private MovementController _movement;
        private ShootController _shoot;

        public KeyboardInput(IInstantiator instantiator)
        {
            _movement = new MovementController();
            _shoot = instantiator.Instantiate<ShootController>();
        }
        
        public void Shot(Transform transformSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shoot.ShotBullet(transformSpawn);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _shoot.ShotLaser(transformSpawn);
            }
            
            _shoot.CheckLaserShot();
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