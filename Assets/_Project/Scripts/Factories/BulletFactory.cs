using Cysharp.Threading.Tasks;
using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Factories.ScriptableObjects;
using GameScene.Interfaces;
using GameScene.Level;
using Zenject;
using GameSystem;

namespace GameScene.Factories
{
    public class BulletFactory : Factory<BulletFactoryData, Bullet, Bullet>
    {
        private const string BulletKey = "Bullet";
        
        private readonly PlayerUI _playerUi;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            BulletFactoryData factoryData,
            GameStateController gameStateController,
            PlayerUI player,
            IAnalyticSystem analyticSystem, 
            LoadPrefab<Bullet> loadPrefab,
            IInstantiator instantiator) : base(factoryData, gameStateController, transformParent, spawnTransform, analyticSystem, loadPrefab, instantiator)
        {
            _playerUi = player;
            
            PoolObjects = new PoolObjects<Bullet>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
        }
        
        public void Respawn()
        {
            PoolObjects.Get();
        }
        
        private async UniTask<Bullet> Preload()
        {
            Bullet bullet = Instantiator.InstantiatePrefabForComponent<Bullet>(
                await LoadPrefab.LoadPrefabFromAddressable(BulletKey), 
                TransformParent.transform);
            bullet.Deactivate();
            return bullet;
        }

        private async void Get(Bullet bullet)
        {
            bullet.Activate(_playerUi.transform.position);
            await bullet.Shot(_playerUi.transform);
            AnalyticSystem.AddBulletShot();
        }
    }
}