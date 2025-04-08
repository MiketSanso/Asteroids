using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;
using GameScene.Level;
using Zenject;

namespace GameScene.Factories
{
    public class BulletFactory : Factory<BulletFactoryData, Bullet>
    {
        private readonly PlayerUI _playerUi;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            IInstantiator instantiator,
            BulletFactoryData factoryData,
            GameStateController gameStateController,
            PlayerUI player) : base(factoryData, gameStateController, transformParent, spawnTransform, instantiator)
        {
            _playerUi = player;
            
            PoolObjects = new PoolObjects<Bullet>(Preload, 
                GetAction, 
                ReturnAction, 
                Data.SizePool);
        }
        
        public void Spawn()
        {
            PoolObjects.Get();
        }
        
        private Bullet Preload()
        {
            Bullet bullet = Instantiator.InstantiatePrefabForComponent<Bullet>(Data.Prefab, TransformParent.transform);
            bullet.Deactivate();
            return bullet;
        }

        private async void GetAction(Bullet bullet)
        {
            bullet.Activate(_playerUi.transform.position);
            await bullet.Shot(_playerUi.transform);
        }
    }
}