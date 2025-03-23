using UnityEngine;
using GameScene.Interfaces;

namespace GameScene.Repositories
{
    public class PoolObjects<T> where T : MonoBehaviour, IPooledObject
    {
        public T[] Objects { get; private set; }

        public PoolObjects(T prefab, int poolSize, Transform transformParent)
        {
            Objects = new T[poolSize];
            
            for (int i = 0; i < poolSize; i++)
            {
                Objects[i] = Object.Instantiate(prefab, transformParent);
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