using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;
using UnityEngine;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class BulletFactory : Factory
    {
        private readonly BulletFactoryData _factoryData;
        private PoolObjects<Bullet> _poolBullets;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            BulletFactoryData factoryData) : base(transformParent, spawnTransform)
        {
            _factoryData = factoryData;
        }
        
        public override void Destroy()
        { }

        protected override void CreatePool()
        {
            _poolBullets = new PoolObjects<Bullet>(_factoryData.Prefab, _factoryData.SizePool, TransformParent.transform);
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