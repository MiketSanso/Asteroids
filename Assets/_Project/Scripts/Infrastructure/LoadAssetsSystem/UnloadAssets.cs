using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameSystem
{
    public class UnloadAssets
    {
        private static List<AsyncOperationHandle> objectsForUnload = new List<AsyncOperationHandle>();

        public void AddAsyncOperationHandleForUnload(AsyncOperationHandle operationHandle)
        {
            objectsForUnload.Add(operationHandle);
        }

        public void UnloadAsset()
        {
            for (int a = 0; a < objectsForUnload.Count; a++)
            {
                Addressables.Release(objectsForUnload[a]);
            }

            objectsForUnload.Clear();
        }
    }
}