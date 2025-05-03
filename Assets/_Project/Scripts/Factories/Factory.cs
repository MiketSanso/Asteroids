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
        public PoolObjects<TTechObj> PoolObjects;
        
        protected TData Data;
        protected IAnalyticService AnalyticService;
        protected LoadPrefab<TSpawnObj> LoadPrefab;
        protected LoadPrefab<Texture2D> LoadSprite;
        protected ConfigSaveService ConfigSaveService;
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
            LoadPrefab<Texture2D> loadSprite,
            IInstantiator instantiator,
            ConfigSaveService configSaveService,
            MusicService musicService)
        {
            GameStateController = gameStateController;
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            AnalyticService = analyticService;
            LoadPrefab = loadPrefab;
            LoadSprite = loadSprite;
            Instantiator = instantiator;
            ConfigSaveService = configSaveService;
            MusicService = musicService;
        }
        
        protected void Return(TTechObj obj) => obj.Deactivate();
    }
}