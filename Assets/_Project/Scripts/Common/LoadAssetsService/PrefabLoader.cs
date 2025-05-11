using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace GameSystem.Common.LoadAssetSystem
{
    public class PrefabLoader<T> where T : Object
    {
        private UnloadAssets _unloadAssets;

        public PrefabLoader(UnloadAssets unloadAssets)
        {
            _unloadAssets = unloadAssets;
        }

        public async UniTask<T> LoadPrefabFromAddressable(string prefabAddress)
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
                else
                {
                    Debug.LogError($"Не удалось загрузить AudioClip: {prefabAddress}");
                    return null;
                }
            }
            else
            {
                var gameObjectHandle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
                await gameObjectHandle.Task;

                if (gameObjectHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var component = gameObjectHandle.Result.GetComponent<T>();
                    if (component != null)
                    {
                        _unloadAssets.AddUnloadElement(gameObjectHandle);
                        return component;
                    }
                    else
                    {
                        Debug.LogError($"На объекте нет компонента типа {typeof(T)}");
                        return null;
                    }
                }
                else
                {
                    Debug.LogError($"Не удалось загрузить GameObject: {prefabAddress}");
                    return null;
                }
            }
        }
    }
}