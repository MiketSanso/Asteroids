using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Repositories.Configs;
using GameScene.Game;
using GameScene.Repositories;
using GameSystem.Common.LoadAssetSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory<TData, TTechObj, TSpawnObj> where TData : Config
        where TTechObj : IPooledObject
        where TSpawnObj : MonoBehaviour
    {
        protected PoolObjects<TTechObj> PoolObjects;
        protected TData Data;
        
        protected readonly IAnalyticService AnalyticService;
        protected readonly AddressablePrefabLoader<TSpawnObj> AddressablePrefabLoader;
        protected readonly ConfigLoadService ConfigLoadService;
        protected readonly IInstantiator Instantiator;
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        protected readonly GameStateController GameStateController;
        protected readonly MusicService MusicService;
        
        protected Factory(GameStateController gameStateController, 
            TransformParent transformParent, 
            SpawnTransform spawnTransform,
            IAnalyticService analyticService, 
            AddressablePrefabLoader<TSpawnObj> addressablePrefabLoader,
            IInstantiator instantiator,
            ConfigLoadService configLoadService,
            MusicService musicService)
        {
            GameStateController = gameStateController;
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            AnalyticService = analyticService;
            AddressablePrefabLoader = addressablePrefabLoader;
            Instantiator = instantiator;
            ConfigLoadService = configLoadService;
            MusicService = musicService;
        }
        
        protected void Return(TTechObj obj) => obj.Deactivate();
    }
}