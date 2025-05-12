using GameScene.Repositories;

namespace GameScene.Common.DataSaveSystem
{
    public interface ILocalSaveService
    {
        public void Save(GameData gameData);
        
        public GameData Load();
    }
}