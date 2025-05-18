using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScene.Models;
using Unity.Services.CloudSave;
using UnityEngine;

namespace GameScene.Common.DataSaveSystem
{
    public class CloudSave : ISaveService
    {
        public async UniTask<bool> Save(DataModel dataModel)
        {
            try
            {
                dataModel.SaveTime = DateTime.Now;
                string jsonKey = JsonUtility.ToJson(dataModel);
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
    
        public async UniTask<DataModel> Load()
        {
            DataModel serverSave = null;
            
            try
            {
                Dictionary<string, string> serverData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {"GameData"} );
                serverSave = JsonUtility.FromJson<DataModel>(serverData["GameData"]);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Не удалось загрузить серверный сейв: " + e.Message);
            }
            
            return serverSave;
        }
    }
}