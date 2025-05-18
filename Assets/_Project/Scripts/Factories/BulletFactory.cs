using Cysharp.Threading.Tasks;
using GameScene.Models;
using GameScene.Entities.Player;
using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using GameScene.Game;
using Zenject;
using GameSystem.Common.LoadAssetSystem;
using UnityEngine;

namespace GameScene.Factories
{
    public class BulletFactory : Factory<BulletFactoryConfig, Bullet>, IInitializable
    {
        private const string BULLET_KEY = "Bullet";
        private const string FACTORY_CONFIG = "BulletFactoryConfig";
        
        private readonly PlayerUI _playerUi;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            GameEventBus gameEventBus,
            PlayerUI player,
            IAnalyticService analyticService, 
            AddressablePrefabLoader<GameObject> addressablePrefabLoader,
            IInstantiator instantiator,
            IConfigLoadService configLoadService,
            MusicService musicService) : base(gameEventBus, transformParent, spawnTransform, analyticService, addressablePrefabLoader, instantiator, configLoadService, musicService)
        {
            _playerUi = player;
        }

        public async void Initialize()
        {
            Data = await ConfigLoadService.Load<BulletFactoryConfig>(FACTORY_CONFIG);
            
            PoolObjects = new PoolObjects<Bullet>(Preload, 
                Get, 
                Return, 
                Data.SizePool);
        }
        
        public async void Respawn()
        {
            await PoolObjects.Get();
        }
        
        private async UniTask<Bullet> Preload()
        {
            Bullet bullet = Instantiator.InstantiatePrefabForComponent<Bullet>(
                await AddressablePrefabLoader.Load(BULLET_KEY), 
                TransformParent.transform);
            
            bullet.Deactivate();
            return bullet;
        }

        private async void Get(Bullet bullet)
        {
            MusicService.Shot();
            bullet.Activate(_playerUi.transform.position);
            await bullet.Shot(_playerUi.transform);
            AnalyticService.AddBulletShot();
        }
    }
}