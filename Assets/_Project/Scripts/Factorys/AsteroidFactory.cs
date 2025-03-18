using GameScene.Repositories;
using UnityEngine;
using GameScene.Level;
using Zenject;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidFactory
    {
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private int _countAsteroids; 
        
        [Inject] private EndPanel _endPanel;
        private int _destroyed;
        private AsteroidsPool _poolAsteroids;

        private void OnDestroy()
        {
            _endPanel.OnRestart -= RestartFly;
            
            foreach (AsteroidUI asteroid in _poolAsteroids.Asteroids)
            {
                asteroid.OnDestroyedWithPoint -= ActivateSmallAsteroids;
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in _poolAsteroids.SmallAsteroids)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
        }

        public void Initialize(AsteroidsPool poolAsteroids)
        {
            _endPanel.OnRestart += RestartFly;
            _poolAsteroids = poolAsteroids;
            RestartFly();

            foreach (AsteroidUI asteroid in _poolAsteroids.Asteroids)
            {
                asteroid.OnDestroyedWithPoint += ActivateSmallAsteroids;
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in _poolAsteroids.SmallAsteroids)
            {
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
        }
        
        private void RestartFly()
        {
            _destroyed = 0;
            
            for (int i = 0; i < _poolAsteroids.SmallAsteroids.Length; i ++)
            {
                if (i < _poolAsteroids.Asteroids.Length)
                {
                    int randomIndex = UnityEngine.Random.Range(0, _transformsSpawn.Length);
                    Transform transformSpawn = _transformsSpawn[randomIndex];
                    _poolAsteroids.Asteroids[i].Activate(transformSpawn);
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
        
        private void ActivateSmallAsteroids(Transform transform)
        {
            int countActivatedAsteroids = 0;
            
            foreach (AsteroidUI asteroid in _poolAsteroids.SmallAsteroids)
            {
                if (countActivatedAsteroids < 2 && !asteroid.gameObject.activeSelf)
                {
                    countActivatedAsteroids++;
                    asteroid.Activate(transform);
                }
                else if (countActivatedAsteroids == 2)
                {
                    break;
                }
            }
        }
    }
}