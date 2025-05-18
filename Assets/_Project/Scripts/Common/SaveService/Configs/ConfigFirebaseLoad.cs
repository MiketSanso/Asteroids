using System;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using GameScene.Models.Configs;
using UnityEngine;

namespace GameScene.Common.ConfigSaveSystem
{
    public class ConfigFirebaseLoad : IConfigLoadService
    {
        public async UniTask<T> Load<T>(string key) where T : Config
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