using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScene.Interfaces;
using GameScene.Repositories;
using Unity.Services.CloudSave;
using UnityEngine;

namespace GameScene.Common.DataSaveSystem
{
    public class CloudSave : SaveService
    {
        protected CloudSave(ILocalSaveService localSaveService) : base(localSaveService)
        { } 
        
        public override async UniTask Save()
        {
            try
            {
                Data.SaveTime = DateTime.Now;
                string jsonKey = JsonUtility.ToJson(Data);
                var data = new Dictionary<string, object> { { "GameData", jsonKey } };
                await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Не удалось сохранить сейв на удалённый сервер: " + e.Message);

                LocalSaveService.Save(Data);
            }
        }
    
        protected override async UniTask Load()
        {
            GameData localSave = LocalSaveService.Load();
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

            if (localSave == null)
            {
                Data = serverSave;
            }
            else if (serverSave == null)
            {
                Data = localSave;
            }
            else
            {
                Data = (serverSave.SaveTime > localSave.SaveTime) ? serverSave : localSave;
            }
        }
    }
}