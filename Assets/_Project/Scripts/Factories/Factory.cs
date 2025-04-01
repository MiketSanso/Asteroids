using GameScene.Level;
using Zenject;

namespace GameScene.Factories
{
    public abstract class Factory
    {
        protected readonly SpawnTransform SpawnTransform;
        protected readonly TransformParent TransformParent;
        protected readonly IInstantiator Instantiator;
        
        protected Factory(TransformParent transformParent, SpawnTransform spawnTransform, IInstantiator instantiator)
        {
            SpawnTransform = spawnTransform;
            TransformParent = transformParent;
            Instantiator = instantiator;
        }
    }
}