using UnityEngine;
using GameScene.Interfaces;
using Zenject;

namespace GameScene.Repositories
{
    public class PoolObjects<T> where T : MonoBehaviour, IPooledObject
    {
        public readonly T[] Objects;

        public PoolObjects(T prefab, int poolSize, Transform transformParent, IInstantiator instantiator)
        {
            Objects = new T[poolSize];
            
            for (int i = 0; i < poolSize; i++)
            {
                Objects[i] = instantiator.InstantiatePrefabForComponent<T>(prefab, transformParent);
                Objects[i].Deactivate();
            }
        }

        public void DeactivateObjects()
        {
            foreach (T poolObject in Objects)
            {
                poolObject.Deactivate();
            }
        }
    }
}