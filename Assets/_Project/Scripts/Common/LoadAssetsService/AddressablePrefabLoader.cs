using Cysharp.Threading.Tasks;
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
            var gameObjectHandle = Addressables.LoadAssetAsync<T>(prefabAddress);
            await gameObjectHandle.Task;

            if (gameObjectHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _unloadAssets.AddUnloadElement(gameObjectHandle);
                return gameObjectHandle.Result;
            }
            
            return null;
        }
    }
}