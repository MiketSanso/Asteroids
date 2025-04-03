using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using GameScene.Interfaces;
using Zenject;

namespace GameScene.Entities.Player
{
    public class Laser : IInitializable
    {
        private LaserRenderer _laserRenderer;
        private LaserData _laserData;
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
            RechargeLaser().Forget();
        }
        
        public async void ShotLaser(Transform transformObject)
        {
            if (CountShotsLaser > 0)
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

                CountShotsLaser -= 1;

                await DisableLine();
            }
        }
        
        public void StartRecharge()
        {
            if (CountShotsLaser < _laserData.MaxCountLaserShoots &&
                _rechargeCompletionSource?.Task.Status == UniTaskStatus.Succeeded)
            {
                RechargeLaser().Forget();
            }
        }
        
        private async UniTask RechargeLaser()
        {
            _rechargeCompletionSource = new UniTaskCompletionSource();
            TimeRechargeLaser =  _laserData.FixedTimeRechargeLaser;
            do
            {
                await UniTask.Delay(TimeSpan.FromSeconds( _laserData.StepTimeRecharge));
                TimeRechargeLaser -=  _laserData.StepTimeRecharge;
                Mathf.Clamp(TimeRechargeLaser, 0,  _laserData.FixedTimeRechargeLaser);
            } while (TimeRechargeLaser > 0);

            CountShotsLaser++;
            _rechargeCompletionSource.TrySetResult();
            if (CountShotsLaser <  _laserData.MaxCountLaserShoots)
                RechargeLaser();
        }

        private async UniTask DisableLine()
        {
            await UniTask.Delay(TimeSpan.FromSeconds( _laserData.TimeVisibleLaser));
            _laserRenderer.LineRenderer.enabled = false;
        }
    }
}