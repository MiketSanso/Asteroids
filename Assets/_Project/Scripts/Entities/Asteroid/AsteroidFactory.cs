using GameScene.Repositories;
using UnityEngine;
using GameScene.Level;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidFactory : MonoBehaviour
    {
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private int _countAsteroids; 
        
        private EndPanel _endPanel;
        private int _destroyed;
        private PoolObjects _poolObjects;

        private void OnDestroy()
        {
            _endPanel.OnRestart -= RestartFly;
            
            foreach (AsteroidUI asteroid in _poolObjects.Asteroids)
            {
                asteroid.OnDestroyedWithPoint -= ActivateSmallAsteroids;
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in _poolObjects.SmallAsteroids)
            {
                asteroid.OnDestroyed -= AddDestroyedAsteroid;
            }
        }

        public void Initialize(PoolObjects poolObjects, EndPanel endPanel)
        {
            _endPanel = endPanel;
            _endPanel.OnRestart += RestartFly;
            _poolObjects = poolObjects;
            RestartFly();

            foreach (AsteroidUI asteroid in _poolObjects.Asteroids)
            {
                asteroid.OnDestroyedWithPoint += ActivateSmallAsteroids;
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
            
            foreach (AsteroidUI asteroid in _poolObjects.SmallAsteroids)
            {
                asteroid.OnDestroyed += AddDestroyedAsteroid;
            }
        }
        
        private void RestartFly()
        {
            _destroyed = 0;
            
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
        
        private void ActivateSmallAsteroids(Transform transform)
        {
            int countActivatedAsteroids = 0;
            
            foreach (AsteroidUI asteroid in _poolObjects.SmallAsteroids)
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