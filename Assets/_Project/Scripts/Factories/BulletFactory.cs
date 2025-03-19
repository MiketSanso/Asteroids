using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;

namespace GameScene.Factories
{
    public class BulletFactory : Factory
    {
        private BulletFactoryData _factoryData;
        private BulletPool _poolBullets;

        public BulletFactory(BulletFactoryData factoryData)
        {
            _factoryData = factoryData;
        }
        
        public override void Destroy()
        {
            
        }

        protected override void CreatePool()
        {
            _poolBullets = new BulletPool(_factoryData.Prefab, _factoryData.SizePool, _factoryData.TransformParent);
        }
        
        private void SpawnBullet()
        {
            foreach (Bullet bullet in _poolBullets.Bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.Activate();
                    break;
                }
            }
        }
    }
}