using GameScene.Factories.ScriptableObjects;
using GameScene.Interfaces;
using GameScene.Level;
using GameScene.Repositories;
using GameSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory<TData, TTechObj, TSpawnObj> where TData : FactoryData
        where TTechObj : IPooledObject
        where TSpawnObj : MonoBehaviour
    {
        public PoolObjects<TTechObj> PoolObjects;
        
        protected TData Data;
        protected readonly IInstantiator Instantiator;
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        protected readonly GameStateController GameStateController;
        protected IAnalyticSystem AnalyticSystem;
        protected LoadPrefab<TSpawnObj> LoadPrefab;
        
        protected Factory(TData data, 
            GameStateController gameStateController, 
            TransformParent transformParent, 
            SpawnTransform spawnTransform,
            IAnalyticSystem analyticSystem, 
            LoadPrefab<TSpawnObj> loadPrefab,
            IInstantiator instantiator)
        {
            GameStateController = gameStateController;
            Data = data;
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            AnalyticSystem = analyticSystem;
            LoadPrefab = loadPrefab;
            Instantiator = instantiator;
        }
        
        protected void Return(TTechObj obj) => obj.Deactivate();
    }
}