using _Project.Scripts.Infrastructure;
using GameScene.Configs;
using GameScene.Interfaces;
using GameScene.Level;
using GameScene.Repositories;
using GameSystem;
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
        protected readonly LoadPrefab<TSpawnObj> LoadPrefab;
        protected readonly ConfigSaveService ConfigSaveService;
        protected readonly IInstantiator Instantiator;
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        protected readonly GameStateController GameStateController;
        protected readonly MusicService MusicService;
        
        protected Factory(GameStateController gameStateController, 
            TransformParent transformParent, 
            SpawnTransform spawnTransform,
            IAnalyticService analyticService, 
            LoadPrefab<TSpawnObj> loadPrefab,
            IInstantiator instantiator,
            ConfigSaveService configSaveService,
            MusicService musicService)
        {
            GameStateController = gameStateController;
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            AnalyticService = analyticService;
            LoadPrefab = loadPrefab;
            Instantiator = instantiator;
            ConfigSaveService = configSaveService;
            MusicService = musicService;
        }
        
        protected void Return(TTechObj obj) => obj.Deactivate();
    }
}