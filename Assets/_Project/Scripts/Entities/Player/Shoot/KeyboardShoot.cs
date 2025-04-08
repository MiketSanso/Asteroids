using UnityEngine;
using GameScene.Factories;
using GameScene.Interfaces;

namespace GameScene.Entities.Player
{
    public class KeyboardShoot : IShoot
    {
        private readonly BulletFactory _bulletFactory;
        private readonly Laser _laser;

        public KeyboardShoot(BulletFactory bulletFactory, Laser laser)
        {
            _bulletFactory = bulletFactory;
            _laser = laser;
        }
        
        public void Shot(Transform transformSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bulletFactory.Spawn();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _laser.Shot(transformSpawn);
            }
        }

        public void CheckLaserShot()
        {
            _laser.StartRecharge();
        }
    }
}