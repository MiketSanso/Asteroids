using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace GameSystem
{
    public class LoadPrefab<T> where T : Object
    {
        private UnloadAssets _unloadAssets;

        public LoadPrefab(UnloadAssets unloadAssets)
        {
            _unloadAssets = unloadAssets;
        }

        public async UniTask<T> LoadPrefabFromAddressable(string prefabAdress)
        {
            if (typeof(T) == typeof(AudioClip))
            {
                var audioClipHandle = Addressables.LoadAssetAsync<AudioClip>(prefabAdress);
                await audioClipHandle.Task;

                if (audioClipHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    return (T)(object)audioClipHandle.Result;
                }
                else
                {
                    Debug.LogError($"Не удалось загрузить AudioClip: {prefabAdress}");
                    return default;
                }
            }
            else
            {
                var gameObjectHandle = Addressables.LoadAssetAsync<GameObject>(prefabAdress);
                await gameObjectHandle.Task;

                if (gameObjectHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var component = gameObjectHandle.Result.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                    else
                    {
                        Debug.LogError($"На объекте нет компонента типа {typeof(T)}");
                        return default;
                    }
                }
                else
                {
                    Debug.LogError($"Не удалось загрузить GameObject: {prefabAdress}");
                    return default;
                }
            }
        }
    }
}