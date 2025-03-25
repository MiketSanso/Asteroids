using GameScene.Entities.Asteroid;
using GameScene.Entities.UFOs;
using GameScene.Factories;
using GameScene.Level;
using UnityEngine;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreInfo
    {
        private AsteroidFactory _asteroidFactory;
        private UfoFactory _ufoFactory;
        private GameStateController _gameStateController;
        
        public float Score { get; private set; }
        
        [Inject]
        public ScoreInfo(AsteroidFactory asteroidFactory,
        UfoFactory ufoFactory,
        GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;
            
            _gameStateController.OnRestart += ResetScore;
            
            foreach (AsteroidUI asteroid in _asteroidFactory.PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (AsteroidUI asteroid in _asteroidFactory.PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (Ufo ufo in _ufoFactory.PoolUfo.Objects)
            {
                ufo.OnDestroyed += AddScore;
            }
        }
        
        private void Destroy()
        {
            _gameStateController.OnRestart -= ResetScore;
            
            foreach (AsteroidUI asteroid in _asteroidFactory.PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed -= AddScore;
            }
            
            foreach (AsteroidUI asteroid in _asteroidFactory.PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed -= AddScore;
            }
            
            foreach (Ufo ufo in _ufoFactory.PoolUfo.Objects)
            {
                ufo.OnDestroyed -= AddScore;
            }
        }
        
        private void AddScore(int scoreSize, Transform transform)
        {
            Score += scoreSize;
        }

        private void ResetScore()
        {
            Score = 0;
        }
    }
}