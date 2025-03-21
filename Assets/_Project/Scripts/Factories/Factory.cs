using GameScene.Factories.ScriptableObjects;

namespace GameScene.Factories
{
    public abstract class Factory
    {
        public abstract void Destroy();
        
        protected abstract void CreatePool();
    }
}