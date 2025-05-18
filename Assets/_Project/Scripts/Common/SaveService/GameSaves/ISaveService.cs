using Cysharp.Threading.Tasks;
using GameScene.Models;

namespace GameScene.Common.DataSaveSystem
{
    public interface ISaveService
    {
        public UniTask<bool> Save(DataModel dataModel);
        
        public UniTask<DataModel> Load();
    }
}