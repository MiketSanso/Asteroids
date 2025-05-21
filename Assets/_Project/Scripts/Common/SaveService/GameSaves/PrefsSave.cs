using System;
using Cysharp.Threading.Tasks;
using GameScene.Models;
using UnityEngine;

namespace GameScene.Common.DataSaveSystem
{
    public class PrefsSave : ISaveService
    {
        private const string NAME_KEY_SAVE = "GameData";
        
        public UniTask<bool> Save(DataModel dataModel)
        {
            dataModel.SaveTime = DateTime.Now;
            string jsonKey = JsonUtility.ToJson(dataModel);
            PlayerPrefs.SetString(NAME_KEY_SAVE, jsonKey);
            PlayerPrefs.Save();
            return new UniTask<bool>(true);
        }
    
        public UniTask<DataModel> Load()
        {
            if (PlayerPrefs.HasKey(NAME_KEY_SAVE))
            {
                string jsonKey = PlayerPrefs.GetString(NAME_KEY_SAVE);
                return JsonUtility.FromJson<UniTask<DataModel>>(jsonKey);
            }

            return new UniTask<DataModel>(null);
        }
    }
}