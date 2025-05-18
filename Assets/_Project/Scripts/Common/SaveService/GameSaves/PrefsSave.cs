using System;
using Cysharp.Threading.Tasks;
using GameScene.Models;
using UnityEngine;

namespace GameScene.Common.DataSaveSystem
{
    public class PrefsSave : ISaveService
    {
        public UniTask<bool> Save(DataModel dataModel)
        {
            dataModel.SaveTime = DateTime.Now;
            string jsonKey = JsonUtility.ToJson(dataModel);
            PlayerPrefs.SetString("GameData", jsonKey);
            PlayerPrefs.Save();
            return new UniTask<bool>(true);
        }
    
        public UniTask<DataModel> Load()
        {
            if (PlayerPrefs.HasKey("GameData"))
            {
                string jsonKey = PlayerPrefs.GetString("GameData");
                return JsonUtility.FromJson<UniTask<DataModel>>(jsonKey);
            }

            return new UniTask<DataModel>(null);
        }
    }
}