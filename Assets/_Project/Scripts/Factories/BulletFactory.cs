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
        private readonly GameStateController _gameStateController;
        private BulletFactoryData _factoryData;
        private PlayerUI _playerUi;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            IInstantiator instantiator,
            BulletFactoryData factoryData,
            PlayerUI player) : base(transformParent, spawnTransform, instantiator)
        {
            _factoryData = factoryData;
            _playerUi = player;
            
            _poolBullets = new PoolObjects<Bullet>(Preload, 
                GetAction, 
                ReturnAction, 
                _factoryData.SizePool);        
        }
        
        public async void Spawn(Transform positionSpawn)
        {
            _poolBullets.Get();
        }
        
          private Bullet Preload()
        {
            Bullet bullet = Instantiator.InstantiatePrefabForComponent<Bullet>(_factoryData.Prefab, TransformParent.transform);
            bullet.Deactivate();
            return bullet;
        }

        private async void GetAction(Bullet bullet)
        {
            bullet.Activate(_playerUi.transform.position);
            await bullet.Shot(_playerUi.transform);
        } 

        private void ReturnAction(Bullet bullet) => bullet.Deactivate();
    }
}