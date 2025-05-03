using _Project.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;
using GameScene.Repositories;
using GameScene.Entities.Player;
using GameScene.Configs;
using GameScene.Interfaces;
using GameScene.Level;
using Zenject;
using GameSystem;
using UnityEngine;

namespace GameScene.Factories
{
    public class BulletFactory : Factory<BulletFactoryConfig, Bullet, Bullet>, IInitializable
    {
        private const string BULLET_KEY = "Bullet";
        private const string FACTORY_CONFIG = "BulletFactoryConfig";
        
        private readonly PlayerUI _playerUi;
        
        public BulletFactory(TransformParent transformParent, 
            SpawnTransform spawnTransform,
            GameStateController gameStateController,
            PlayerUI player,
            IAnalyticService analyticService, 
            LoadPrefab<Bullet> loadPrefab,
            LoadPrefab<Texture2D> loadSprite,
            IInstantiator instantiator,
            ConfigSaveService configSaveService,
            MusicService musicService) : base(gameStateController, transformParent, spawnTransform, analyticService, loadPrefab, loadSprite, instantiator, configSaveService, musicService)
        {
            _playerUi = player;
        }

        public async void Initialize()
        {
            Data = await ConfigSaveService.Load<BulletFactoryConfig>(FACTORY_CONFIG);
            
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
                await LoadPrefab.LoadPrefabFromAddressable(BULLET_KEY), 
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