using System;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace GameScene.Infrastructure.ConfigSaveSystem
{
    public class ConfigFirebaseSave : ConfigSaveService
    {
        public override async UniTask<T> Load<T>(string key)
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            
            await remoteConfig.FetchAsync(TimeSpan.Zero);
            await remoteConfig.ActivateAsync();
            
            string jsonConfig = remoteConfig.GetValue(key).StringValue;
            if (string.IsNullOrEmpty(jsonConfig))
                throw new Exception("Empty config received" + " ");
            
            return JsonUtility.FromJson<T>(jsonConfig);
        }
    }
}