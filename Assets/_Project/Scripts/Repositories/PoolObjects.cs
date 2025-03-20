using UnityEngine;
using Zenject;
using GameScene.Interfaces;
using GameScene.Level;

namespace GameScene.Repositories
{
    public class PoolObjects<T> where T : MonoBehaviour, IPooledObject
    {
        private SpawnTransform _spawnTransform;

        public T[] Objects { get; private set; }

        public PoolObjects(T prefab, int poolSize, Transform transformParent)
        {
            Objects = new T[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                Objects[i] = Create(prefab, GetRandomTransform(), transformParent);
            }
        }

        public void DeactivateObjects()
        {
            foreach (T poolObject in Objects)
            {
                poolObject.Deactivate();
            }
        }
        
        [Inject]
        public void Construct(SpawnTransform spawnTransform)
        {
            _spawnTransform = spawnTransform;
        }

        private T Create(T prefab, Vector2 positionSpawn, Transform transformParent)
        {
            T newObject = Object.Instantiate(prefab, positionSpawn, Quaternion.identity, transformParent);
            newObject.Deactivate();
            return newObject;
        }

        public Vector2 GetRandomTransform()
        {
            return _spawnTransform.GetPosition();
        }
    }
}