using UnityEngine;
using GameScene.Factories;

namespace GameScene.Entities.Player
{
    public class ShootController
    {
        private readonly BulletFactory _bulletFactory;
        private readonly Laser _laser;

        public ShootController(BulletFactory bulletFactory, Laser laser)
        {
            _bulletFactory = bulletFactory;
            _laser = laser;
        }
        
        public void ShotBullet(Transform transformSpawn)
        {
            _bulletFactory.Respawn();
        }
        
        public void ShotLaser(Transform transformSpawn)
        {
            _laser.Shot(transformSpawn);
        }

        public void CheckLaserShot()
        {
            _laser.StartRecharge();
        }
    }
}