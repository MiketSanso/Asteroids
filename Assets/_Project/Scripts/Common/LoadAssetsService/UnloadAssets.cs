using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace GameSystem.Common.LoadAssetSystem
{
    public class UnloadAssets : IInitializable, IDisposable
    {
        private List<AsyncOperationHandle> _objectsForUnload;
    
        public void Initialize()
        {
            _objectsForUnload = new List<AsyncOperationHandle>();
        }

        public void Dispose()
        {
            UnloadAllAssets();
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