using GameScene.Level;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory
    {
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        
        protected Factory(TransformParent transformParent, SpawnTransform spawnTransform)
        {
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
        }
        
        public abstract void Destroy();
    }
}