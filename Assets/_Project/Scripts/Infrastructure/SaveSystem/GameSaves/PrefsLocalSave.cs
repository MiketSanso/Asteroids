using System;
using GameScene.Interfaces;
using GameScene.Repositories;
using UnityEngine;

namespace GameScene.Infrastructure.DataSaveSystem
{
    public class PrefsLocalSave : ILocalSaveService
    {
        public void Save(GameData data)
        {
            data.SaveTime = DateTime.Now;
            string jsonKey = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("GameData", jsonKey);
            PlayerPrefs.Save();
        }
    
        public GameData Load()
        {
            if (PlayerPrefs.HasKey("GameData"))
            {
                string jsonKey = PlayerPrefs.GetString("GameData");
                return JsonUtility.FromJson<GameData>(jsonKey);
            }

            return new GameData();
        }
    }
}