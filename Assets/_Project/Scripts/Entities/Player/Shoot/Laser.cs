using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using GameScene.Interfaces;
using Zenject;

namespace GameScene.Entities.Player
{
    public class Laser : IInitializable
    {
        private readonly LaserRenderer _laserRenderer;
        private readonly LaserData _laserData;
        private UniTaskCompletionSource _rechargeCompletionSource;
        
        public float TimeRechargeLaser { get; private set; }
        public int CountShotsLaser { get; private set; }

        public Laser(LaserData laserData, LaserRenderer laserRenderer)
        {
            _laserRenderer = laserRenderer;
            _laserData = laserData;
        }
        
        public void Initialize()
        {
            _laserRenderer.LineRenderer.enabled = false;
            Recharge().Forget();
        }
        
        public async void Shot(Transform transformObject)
        {
            if (CountShotsLaser > 0)
            {
                float timesShot = 0;
                
                do
                {
                    Vector2 direction = transformObject.up;

                    _laserRenderer.LineRenderer.SetPosition(0, transformObject.position);
                    _laserRenderer.LineRenderer.SetPosition(1, (Vector2)transformObject.position + direction * _laserData.LaserRange);
                    _laserRenderer.LineRenderer.enabled = true;

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

                await DisableLine();
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

        private async UniTask DisableLine()
        {
            _laserRenderer.LineRenderer.enabled = false;
        }
    }
}