using GameScene.Repositories;

namespace GameScene.Interfaces
{
    public interface ILocalSaveService
    {
        public void Save(GameData data);
        public GameData Load();
    }
}