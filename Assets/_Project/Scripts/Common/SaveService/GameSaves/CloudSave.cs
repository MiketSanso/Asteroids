using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScene.Repositories;
using Unity.Services.CloudSave;
using UnityEngine;

namespace GameScene.Common.DataSaveSystem
{
    public class CloudSave : IGlobalSaveService
    {
        public async UniTask<bool> Save(GameData gameData)
        {
            try
            {
                gameData.SaveTime = DateTime.Now;
                string jsonKey = JsonUtility.ToJson(gameData);
                var data = new Dictionary<string, object> { { "GameData", jsonKey } };
                await CloudSaveService.Instance.Data.ForceSaveAsync(data);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Не удалось сохранить сейв на удалённый сервер: " + e.Message);
                return false;
            }
        }
    
        public async UniTask<GameData> Load()
        {
            GameData serverSave = null;
            
            try
            {
                Dictionary<string, string> serverData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {"GameData"} );
                serverSave = JsonUtility.FromJson<GameData>(serverData["GameData"]);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Не удалось загрузить серверный сейв: " + e.Message);
            }
            
            return serverSave;
        }
    }
}