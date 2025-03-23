using GameScene.Level;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory
    {
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;

        [Inject]
        public Factory(TransformParent transformParent, SpawnTransform spawnTransform)
        {
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            
            CreatePool();
        }
        
        public abstract void Destroy();
        
        protected abstract void CreatePool();
    }
}