using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;
using UnityEngine;

namespace GameScene.Factories
{
    public class BulletFactory : Factory
    {
        private readonly BulletFactoryData _factoryData;
        private PoolObjects<Bullet> _poolBullets;

        public BulletFactory(BulletFactoryData factoryData)
        {
            _factoryData = factoryData;
        }
        
        public override void Destroy()
        { }

        protected override void CreatePool()
        {
            _poolBullets = new PoolObjects<Bullet>(_factoryData.Prefab, _factoryData.SizePool, _factoryData.TransformParent.Transform);
        }
        
        public void SpawnBullet()
        {
            foreach (Bullet bullet in _poolBullets.Objects)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.Activate(Vector2.zero);
                    break;
                }
            }
        }
    }
}