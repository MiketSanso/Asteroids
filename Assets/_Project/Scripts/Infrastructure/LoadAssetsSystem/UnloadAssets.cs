using System.Collections.Generic;
using GameScene.Infrastructure;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace GameSystem.Infrastructure.LoadAssetSystem
{
    public class UnloadAssets : IInitializable
    {
        private List<AsyncOperationHandle> _objectsForUnload;

        private readonly GameStateController _gameStateController;

        public UnloadAssets(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        public void Initialize()
        {
            _objectsForUnload = new List<AsyncOperationHandle>();
            _gameStateController.OnClose += UnloadAllAssets;
        }
        
        public void AddUnloadElement(AsyncOperationHandle operationHandle)
        {
            _objectsForUnload.Add(operationHandle);
        }

        private void UnloadAllAssets()
        {
            foreach (AsyncOperationHandle objectForUnload in _objectsForUnload)
            {
                Addressables.Release(objectForUnload);
            }

            _objectsForUnload.Clear();
        }
    }
}