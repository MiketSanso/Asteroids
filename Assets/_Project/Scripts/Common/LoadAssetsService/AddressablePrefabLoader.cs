using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace GameSystem.Common.LoadAssetSystem
{
    public class AddressablePrefabLoader<T> where T : Object
    {
        private UnloadAssets _unloadAssets;

        public AddressablePrefabLoader(UnloadAssets unloadAssets)
        {
            _unloadAssets = unloadAssets;
        }

        public async UniTask<T> Load(string prefabAddress)
        {
            if (typeof(T) == typeof(AudioClip))
            {
                var audioClipHandle = Addressables.LoadAssetAsync<AudioClip>(prefabAddress);
                await audioClipHandle.Task;

                if (audioClipHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    _unloadAssets.AddUnloadElement(audioClipHandle);
                    return (T)(object)audioClipHandle.Result;
                }
            }
            else
            {
                var gameObjectHandle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
                await gameObjectHandle.Task;

                if (gameObjectHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var component = gameObjectHandle.Result.GetComponent<T>();

                    _unloadAssets.AddUnloadElement(gameObjectHandle);
                    return component;
                }
            }
            
            return null;
        }
    }
}