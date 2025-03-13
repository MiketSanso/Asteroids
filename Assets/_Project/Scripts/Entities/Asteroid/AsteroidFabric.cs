using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidFabric : MonoBehaviour
    {
        [SerializeField] private float _minSpeedAsteroid;
        [SerializeField] private float _maxSpeedAsteroid;
        [SerializeField] private int _countAsteroids;
        [SerializeField] private int _countSmallAsteroids;
        [SerializeField] private int _destroyed;
        [SerializeField] private Asteroid _prefab;
        [SerializeField] private Asteroid _smallPrefab;
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private Transform _transformParent;
        
        private AsteroidsPool _asteroidsPool;

        public void Start()
        {
            CreateAsteroids();
        }
        
        private void CreateAsteroids()
        {
            Transform transformSpawn = _transformsSpawn[Random.Range(0, _transformsSpawn.Length)];
            _asteroidsPool = new AsteroidsPool(_prefab, _countAsteroids, transformSpawn, _transformParent);

            RespawnAsteroids();
        }

        private void RespawnAsteroids()
        {
            foreach (Asteroid asteroid in _asteroidsPool.Asteroids)
            {
                float vectorX = Random.Range(_minSpeedAsteroid, _maxSpeedAsteroid);
                float vectorY = Random.Range(_minSpeedAsteroid, _maxSpeedAsteroid);
                asteroid.Activate(new Vector2(vectorX, vectorY));
            }
        }

        private void AddDestroyedAsteroid()
        {
            _destroyed++;

            if (_destroyed == _countAsteroids + _countSmallAsteroids)
            {
                
            }
        }
    }
}