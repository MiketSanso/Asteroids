using _Project.Scripts.Entities;
using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidFabric : MonoBehaviour
    {
        [SerializeField] private Vector2 _speed;
        [SerializeField] private Vector2 _speedSmall;
        [SerializeField] private AsteroidUI _prefab;
        [SerializeField] private AsteroidUI _smallPrefab;
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private Transform _transformParent;
        [SerializeField] private float _sprayVelocity;
        [SerializeField] private int _countAsteroids;
        
        private int _destroyed;
        private PoolObjects _poolObjects;

        private void OnDestroy()
        {
            _poolObjects.Destroy();
            _poolObjects.OnAsteroidDestroyed -= AddDestroyedAsteroid;
        }

        public void Initialize(PoolObjects poolObjects)
        {
            _poolObjects = poolObjects;
            RestartFly();
        }
        
        public void RestartFly()
        {
            for (int i = 0; i < _poolObjects.SmallAsteroids.Length; i ++)
            {
                if (i < _poolObjects.Asteroids.Length)
                {
                    int randomIndex = UnityEngine.Random.Range(0, _transformsSpawn.Length);
                    Transform transformSpawn = _transformsSpawn[randomIndex];
                    _poolObjects.Asteroids[i].Activate(transformSpawn);
                }
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