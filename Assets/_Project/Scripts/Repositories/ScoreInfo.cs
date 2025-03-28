using GameScene.Entities.Asteroid;
using GameScene.Entities.UFOs;
using GameScene.Factories;
using GameScene.Level;
using UnityEngine;

namespace GameScene.Repositories
{
    public class ScoreInfo
    {
        private readonly AsteroidFactory _asteroidFactory;
        private readonly UfoFactory _ufoFactory;
        private readonly GameStateController _gameStateController;
        
        public float Score { get; private set; }
        
        public ScoreInfo(AsteroidFactory asteroidFactory,
        UfoFactory ufoFactory,
        GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;

            Initialize();
        }

        private void Initialize()
        {
            _gameStateController.OnRestart += ResetScore;
            _gameStateController.OnCloseGame += Destroy;
            
            foreach (AsteroidUI asteroid in _asteroidFactory.PoolAsteroids.Objects)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (AsteroidUI asteroid in _asteroidFactory.PoolSmallAsteroids.Objects)
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
            _gameStateController.OnCloseGame -= Destroy;
            
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