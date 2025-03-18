using System;
using GameScene.Repositories;
using UnityEngine;
using Cysharp.Threading.Tasks;
using GameScene.Interfaces;

namespace GameScene.Entities.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform _transformSpawn;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _laserRange;
        [SerializeField] private float _timeVisibleLaser;
        [SerializeField] private float _speed;
        [SerializeField] private float _fixedTimeRechargeLaser;
        [SerializeField] private float _stepTimeRecharge;
        [SerializeField] private int _maxCountLaserShoots;
        
        private BulletPool _poolBullets;
        private UniTaskCompletionSource _rechargeCompletionSource;
        
        public float TimeRechargeLaser { get; private set; }
        public int CountShotsLaser { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnBullet();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShootLaser();
            }
        }

        private async void Start()
        {
            _lineRenderer.enabled = false;
            _rechargeCompletionSource = new UniTaskCompletionSource();
            await RechargeLaser();
        }

        public void Initialize(BulletPool poolBullets)
        {
            _poolBullets = poolBullets;
        }
        
        private async void ShootLaser()
        {
            if (CountShotsLaser > 0)
            {
                Vector2 direction = transform.up;
            
                _lineRenderer.SetPosition(0, _transformSpawn.position);
                _lineRenderer.SetPosition(1, (Vector2)_transformSpawn.position + direction * _laserRange);
                _lineRenderer.enabled = true;
            
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, _laserRange);
            
                Debug.DrawRay(transform.position, direction * _laserRange, Color.red, 0.5f);
            
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out IDestroyableEnemy enemy))
                    {
                        enemy.Destroy(false);
                    }
                }

                CountShotsLaser -= 1;

                if (_rechargeCompletionSource.Task.Status.IsCompleted())
                {
                    _rechargeCompletionSource = new UniTaskCompletionSource();
                    await RechargeLaser();
                }

                await DisableLine();
            }
        }
        
        private async UniTask DisableLine()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_timeVisibleLaser));
            _lineRenderer.enabled = false;
        }
        
        private async UniTask RechargeLaser()
        {
            do
            {
                TimeRechargeLaser = _fixedTimeRechargeLaser;
                
                do
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_stepTimeRecharge));
                    if (_stepTimeRecharge < 0)
                    {
                        Debug.LogError("Шаг времени перезарядки - отрицательное значение!");
                    }
                    
                    TimeRechargeLaser -= _stepTimeRecharge;
                    Mathf.Clamp(TimeRechargeLaser, 0, _fixedTimeRechargeLaser);

                } while (TimeRechargeLaser > 0);

                CountShotsLaser += 1;

            } while (CountShotsLaser < _maxCountLaserShoots);
            
            _rechargeCompletionSource.TrySetResult();
        }

        private async UniTask SpawnBullet()
        {
            foreach (Bullet bullet in _poolBullets.Bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.Activate();
                    break;
                }
            }
        }
    }
}