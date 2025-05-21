using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using GameScene.Game;
using GameScene.Models;
using GameSystem.Common.LoadAssetSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory<TData, TTechObj> where TData : Config
        where TTechObj : IPooledObject
    {
        protected PoolObjects<TTechObj> PoolObjects;
        protected TData Data;
        
        protected readonly IAnalyticService AnalyticService;
        protected readonly AddressablePrefabLoader<GameObject> AddressablePrefabLoader;
        protected readonly IConfigLoadService ConfigLoadService;
        protected readonly IInstantiator Instantiator;
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        protected readonly GameEndController GameStateController;
        protected readonly MusicService MusicService;
        
        protected Factory(GameEndController gameEndController, 
            TransformParent transformParent, 
            SpawnTransform spawnTransform,
            IAnalyticService analyticService, 
            AddressablePrefabLoader<GameObject> addressablePrefabLoader,
            IInstantiator instantiator,
            IConfigLoadService configLoadService,
            MusicService musicService)
        {
            GameStateController = gameEndController;
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