using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;
using UnityEngine;
using GameScene.Level;
using Zenject;

namespace GameScene.Factories
{
    public class BulletFactory : Factory
    {
        private readonly PoolObjects<Bullet> _poolBullets;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            BulletFactoryData factoryData,
            IInstantiator instantiator) : base(transformParent, spawnTransform)
        {
            _poolBullets = new PoolObjects<Bullet>(factoryData.Prefab, factoryData.SizePool, TransformParent.transform, instantiator);
        }
        
        public override void Destroy()
        { }
        
        public async void SpawnBullet(Transform positionSpawn)
        {
            foreach (Bullet bullet in _poolBullets.Objects)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    await bullet.Shot(positionSpawn);
                    break;
                }
            }
        }
    }
}