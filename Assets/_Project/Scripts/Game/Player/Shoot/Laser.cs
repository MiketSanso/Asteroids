using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using GameScene.Common;
using Zenject;

namespace GameScene.Entities.PlayerSpace
{
    public class Laser : IInitializable
    {
        private string LASER_CONFIG = "LaserConfig";
        
        public delegate void DeactivateEventHandler(Vector2 firstPosition, Vector2 secondPosition);
        public event DeactivateEventHandler OnActivateLaser;
        public event Action OnDeactivateLaser;
        
        private UniTaskCompletionSource _rechargeCompletionSource;
        private LaserConfig _laserData;
        
        private readonly IAnalyticService _analyticService;
        private readonly IConfigLoadService _configLoadService;
        
        public float TimeRechargeLaser { get; private set; }
        public int CountShotsLaser { get; private set; }

        public Laser(IConfigLoadService configLoadService, IAnalyticService analyticService)
        {
            _configLoadService = configLoadService;
            _analyticService = analyticService;
        }
        
        public async void Initialize()
        {
            _laserData = await _configLoadService.Load<LaserConfig>(LASER_CONFIG);
            
            await Recharge();
        }
        
        public async void Shot(Transform transformObject)
        {
            if (CountShotsLaser > 0)
            {
                CountShotsLaser -= 1;

                _analyticService.UseLaser();
                float timesShot = 0;
                
                while (timesShot <= _laserData.TimeVisibleLaser)
                {
                    Vector2 direction = transformObject.up;
                    
                    OnActivateLaser?.Invoke(transformObject.position, (Vector2)transformObject.position + direction * _laserData.LaserRange);

                    RaycastHit2D[] hits = new RaycastHit2D[10];
                    int hitCount = Physics2D.RaycastNonAlloc(transformObject.position, direction, hits,  _laserData.LaserRange);

                    Debug.DrawRay(transformObject.position, direction *  _laserData.LaserRange, Color.red, 0.5f);

                    for (int i = 0; i < hitCount; i++)
                    {
                        var hit = hits[i];
                        if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out IDestroyableEnemy enemy))
                        {
                            enemy.Destroy();
                        }
                    }
                    
                    await UniTask.Delay(TimeSpan.FromSeconds(_laserData.StepTimeDamage));
                    timesShot += _laserData.StepTimeDamage; 
                } 

                OnDeactivateLaser?.Invoke();
            }
        }
        
        public void StartRecharge()
        {
            if (CountShotsLaser < _laserData.MaxCountLaserShoots &&
                _rechargeCompletionSource?.Task.Status == UniTaskStatus.Succeeded)
            {
                Recharge().Forget();
            }
        }
        
        private async UniTask Recharge()
        {
            _rechargeCompletionSource = new UniTaskCompletionSource();
            TimeRechargeLaser =  _laserData.FixedTimeRechargeLaser;
            
            while (TimeRechargeLaser > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds( _laserData.StepTimeRecharge));
                TimeRechargeLaser -=  _laserData.StepTimeRecharge;
            }

            CountShotsLaser++;
            _rechargeCompletionSource.TrySetResult();
            if (CountShotsLaser <  _laserData.MaxCountLaserShoots)
                Recharge().Forget();
        }
    }
}