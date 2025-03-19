using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory
    {
        private int _destroyed;
        private int _countAsteroids; 
        private AsteroidsPool _poolAsteroids;
        private EndPanel _endPanel;
        
        [Inject]
        public virtual void Construct(EndPanel endPanel)
        {
            _endPanel = endPanel;
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
        
        public override void Destroy()
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
        
        private void RestartFly()
        {
            _destroyed = 0;
            
            for (int i = 0; i < _poolAsteroids.SmallAsteroids.Length; i ++)
            {
                if (i < _poolAsteroids.Asteroids.Length)
                {
                    _poolAsteroids.Asteroids[i].Activate(_poolAsteroids.GetRandomTransform());
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
                    asteroid.Activate(transform.position);
                }
                else if (countActivatedAsteroids == 2)
                {
                    break;
                }
            }
        }
    }
}