using GameScene.Repositories;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public abstract class SaveService : IInitializable
    {
        public GameData Data;
        
        public void Initialize()
        {
            Data = new GameData();
            Load();
        }
        
        public abstract void Save();
        
        protected abstract void Load();
    }
}