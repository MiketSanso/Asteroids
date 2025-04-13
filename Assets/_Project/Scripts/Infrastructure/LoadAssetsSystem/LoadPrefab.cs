using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
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
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(prefabAdress);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                T component = handle.Result.GameObject().GetComponent<T>();
                if (component != null)
                {
                    _unloadAssets.AddAsyncOperationHandleForUnload(handle);
                    return component;
                }
                else
                {
                    Debug.LogError("Prefab is load, but dont have a need component");
                }
            }
            else
            {
                Debug.LogError("Failed to load prefab: " + handle.Status);
            }
            return null;
        }
    }
}