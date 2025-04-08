using GameScene.Factories.ScriptableObjects;
using GameScene.Interfaces;
using GameScene.Level;
using GameScene.Repositories;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory<TData, TObj> where TData : FactoryData, 
        new() where TObj : IPooledObject
    {
        public PoolObjects<TObj> PoolObjects;
        
        protected TData Data;
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        protected readonly IInstantiator Instantiator;
        protected readonly GameStateController GameStateController;
        
        protected Factory(TData data, GameStateController gameStateController, TransformParent transformParent, SpawnTransform spawnTransform, IInstantiator instantiator)
        {
            GameStateController = gameStateController;
            Data = data;
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            Instantiator = instantiator;
        }
        
        protected void ReturnAction(TObj bullet) => bullet.Deactivate();
    }
}