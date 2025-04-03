using GameScene.Entities.Asteroid;
using GameScene.Entities.UFOs;
using GameScene.Factories;
using GameScene.Level;
using UnityEngine;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreInfo : IInitializable
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
        }

        public void Initialize()
        {
            _gameStateController.OnRestart += ResetScore;
            _gameStateController.OnCloseGame += Destroy;
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolAsteroids.Pool)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolSmallAsteroids.Pool)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (Ufo ufo in _ufoFactory.PoolUfo.Pool)
            {
                ufo.OnDestroyed += AddScore;
            }
        }
        
        private void Destroy()
        {
            _gameStateController.OnRestart -= ResetScore;
            _gameStateController.OnCloseGame -= Destroy;
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolAsteroids.Pool)
            {
                asteroid.OnDestroyed -= AddScore;
            }
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolAsteroids.Pool)
            {
                asteroid.OnDestroyed -= AddScore;
            }
            
            foreach (Ufo ufo in _ufoFactory.PoolUfo.Pool)
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