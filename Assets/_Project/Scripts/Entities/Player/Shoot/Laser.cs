using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using GameScene.Interfaces;
using Zenject;

namespace GameScene.Entities.Player
{
    public class Laser : IInitializable
    {
        public delegate void DeactivateEventHandler(Vector2 firstPosition, Vector2 secondPosition);
        public event DeactivateEventHandler OnActivateLaser;
        public event Action OnDeactivateLaser;
        
        private UniTaskCompletionSource _rechargeCompletionSource;
        private IAnalyticService _analyticService;
        
        private readonly LaserData _laserData;
        
        public float TimeRechargeLaser { get; private set; }
        public int CountShotsLaser { get; private set; }

        public Laser(LaserData laserData, IAnalyticService analyticService)
        {
            _analyticService = analyticService;
            _laserData = laserData;
        }
        
        public void Initialize()
        {
            Recharge().Forget();
        }
        
        public async void Shot(Transform transformObject)
        {
            if (CountShotsLaser > 0)
            {
                _analyticService.UseLaser();
                float timesShot = 0;
                
                do
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
                while (timesShot <= _laserData.TimeVisibleLaser);
                
                CountShotsLaser -= 1;

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
            do
            {
                await UniTask.Delay(TimeSpan.FromSeconds( _laserData.StepTimeRecharge));
                TimeRechargeLaser -=  _laserData.StepTimeRecharge;
            } while (TimeRechargeLaser > 0);

            CountShotsLaser++;
            _rechargeCompletionSource.TrySetResult();
            if (CountShotsLaser <  _laserData.MaxCountLaserShoots)
                Recharge().Forget();
        }
    }
}