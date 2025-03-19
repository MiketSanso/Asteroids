using GameScene.Repositories;
using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Factories.ScriptableObjects;
using Zenject;
using GameScene.Level;

namespace GameScene.Factories
{
    public class AsteroidFactory : Factory
    {
        private AsteroidFactoryData _factoryData;
        private int _destroyed;
        private int _countAsteroids; 
        private AsteroidsPool _poolAsteroids;
        private EndPanel _endPanel;
        
        public AsteroidFactory(AsteroidFactoryData factoryData)
        {
            _factoryData = factoryData;
            _endPanel.OnRestart += RestartFly;
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
        
        [Inject]
        public virtual void Construct(EndPanel endPanel)
        {
            _endPanel = endPanel;
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
        
        protected override void CreatePool()
        {
            _poolAsteroids = new AsteroidsPool(_factoryData.SmallAsteroidData, 
                _factoryData.AsteroidData, 
                _factoryData.SizePool, 
                _factoryData.TransformParent);
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