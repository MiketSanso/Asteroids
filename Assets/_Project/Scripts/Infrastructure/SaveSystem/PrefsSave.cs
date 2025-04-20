using GameScene.Repositories;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class PrefsSave : SaveService
    {
        public override void Save()
        {
            string jsonKey = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString("keySave", jsonKey);
            PlayerPrefs.Save();
        }
    
        protected override void Load()
        {
            if (PlayerPrefs.HasKey("keySave"))
            {
                string jsonKey = PlayerPrefs.GetString("keySave");
                Data = JsonUtility.FromJson<GameData>(jsonKey);
            }
        }
    }
}