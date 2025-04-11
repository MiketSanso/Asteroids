using UnityEngine;
using Zenject;

namespace GameScene.Repositories
{
    public class GameData : IInitializable
    {
        public Data Data;
    
        public void Initialize()
        {           
            Data = new Data();
            Load();
        }

        public void Save()
        {
            string jsonKey = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString("keySave", jsonKey);
            PlayerPrefs.Save();
        }
    
        private void Load()
        {
            if (PlayerPrefs.HasKey("keySave"))
            {
                string jsonKey = PlayerPrefs.GetString("keySave");
                Data = JsonUtility.FromJson<Data>(jsonKey);
            }
        }
    }
    
    public class Data
    {
        public float MaxScore = 0;
    }
}
