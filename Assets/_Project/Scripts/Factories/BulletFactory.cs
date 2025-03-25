using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;
using UnityEngine;
using GameScene.Level;

namespace GameScene.Factories
{
    public class BulletFactory
    {
        private readonly SpawnTransform SpawnTransform;
        private readonly TransformParent TransformParent;
        private readonly BulletFactoryData _factoryData;
        private PoolObjects<Bullet> _poolBullets;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            BulletFactoryData factoryData)
        {
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            _factoryData = factoryData;
            
            _poolBullets = new PoolObjects<Bullet>(_factoryData.Prefab, _factoryData.SizePool, TransformParent.transform);
        }
        
        public void Destroy()
        { }
        
        public async void SpawnBullet(Vector2 positionSpawn)
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