using UnityEngine;
using Zenject;
using GameScene.Level;

namespace GameScene.Repositories
{
    public abstract class PoolObjects
    {
        private SpawnTransform _spawnTransform;
        
        [Inject]
        public void Construct(SpawnTransform spawnTransform)
        {
            _spawnTransform = spawnTransform;
        }
        
        public Vector2 GetRandomTransform()
        {
            return _spawnTransform.GetPosition();
        }

        public abstract void DeactivateObjects();
    }
}