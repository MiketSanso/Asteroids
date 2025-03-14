using System;
using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidFabric : MonoBehaviour
    {
        [SerializeField] private Vector2 _speed;
        [SerializeField] private Vector2 _speedSmall;
        [SerializeField] private int _countAsteroids;
        [SerializeField] private Asteroid _prefab;
        [SerializeField] private Asteroid _smallPrefab;
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private Transform _transformParent;
        
        private int _destroyed;
        private AsteroidsPool _asteroidsPool;

        private void Start()
        {
            CreateAsteroids();
        }

        private void OnDestroy()
        {
            _asteroidsPool.Destroy();
            _asteroidsPool.OnAsteroidDestroyed -= AddDestroyedAsteroid;
        }

        private void CreateAsteroids()
        {
            _asteroidsPool = new AsteroidsPool(_prefab, _countAsteroids, _transformsSpawn, _transformParent, _smallPrefab, _speed, _speedSmall);
            _asteroidsPool.OnAsteroidDestroyed += AddDestroyedAsteroid;

            RestartFly();
        }
        
        private void RestartFly()
        {
            for (int i = 0; i < _asteroidsPool.SmallAsteroids.Length; i ++)
            {
                if (i < _asteroidsPool.Asteroids.Length)
                {
                    int randomIndex = UnityEngine.Random.Range(0, _transformsSpawn.Length);
                    Transform transformSpawn = _transformsSpawn[randomIndex];
                    _asteroidsPool.Asteroids[i].Activate(transformSpawn);
                }
                
                _asteroidsPool.SmallAsteroids[i].Deactivate();
            }
        }
        
        private void AddDestroyedAsteroid()
        {
            _destroyed++;

            if (_destroyed == _countAsteroids * 3)
            {
                _destroyed = 0;
                RestartFly();
            }
        }
    }
}