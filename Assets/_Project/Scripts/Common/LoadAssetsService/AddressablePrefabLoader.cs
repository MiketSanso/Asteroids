using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace GameSystem.Common.LoadAssetSystem
{
    public class AddressablePrefabLoader<T> where T : Object
    {
        private readonly UnloadAssets _unloadAssets;

        public AddressablePrefabLoader(UnloadAssets unloadAssets)
        {
            _unloadAssets = unloadAssets;
        }
        
        public async UniTask<T> Load(string prefabAddress)
        {
            try
            {
                Debug.Log($"Trying to load addressable: {prefabAddress}");
                var gameObjectHandle = Addressables.LoadAssetAsync<T>(prefabAddress);
                await gameObjectHandle.Task;

                if (gameObjectHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"Successfully loaded: {prefabAddress}");
                    _unloadAssets.AddUnloadElement(gameObjectHandle);
                    return gameObjectHandle.Result;
                }
        
                Debug.LogError($"Failed to load: {prefabAddress}. Status: {gameObjectHandle.Status}");
                return null;
            }
            catch (Exception e)
            {
                Debug.LogError($"Addressable load error for {prefabAddress}: {e}");
                return null;
            }
        }
    }
}