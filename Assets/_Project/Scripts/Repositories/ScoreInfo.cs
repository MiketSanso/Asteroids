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
        
        private readonly GameData _gameData;
        
        public float Score { get; private set; }
        
        public ScoreInfo(AsteroidFactory asteroidFactory,
        UfoFactory ufoFactory,
        GameStateController gameStateController,
        GameData gameData)
        {
            _gameStateController = gameStateController;
            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;
            _gameData = gameData;
        }

        public void Initialize()
        {
            _gameStateController.OnRestart += ResetScore;
            _gameStateController.OnFinish += CheckScoreRecord;
            _gameStateController.OnCloseGame += Destroy;
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolObjects.Pool)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolSmallObjects.Pool)
            {
                asteroid.OnDestroyed += AddScore;
            }
            
            foreach (Ufo ufo in _ufoFactory.PoolObjects.Pool)
            {
                ufo.OnDestroyed += AddScore;
            }
        }
        
        private void Destroy()
        {
            _gameStateController.OnRestart -= ResetScore;
            _gameStateController.OnFinish -= CheckScoreRecord;
            _gameStateController.OnCloseGame -= Destroy;
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolObjects.Pool)
            {
                asteroid.OnDestroyed -= AddScore;
            }
            
            foreach (Asteroid asteroid in _asteroidFactory.PoolSmallObjects.Pool)
            {
                asteroid.OnDestroyed -= AddScore;
            }
            
            foreach (Ufo ufo in _ufoFactory.PoolObjects.Pool)
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
        
        private void CheckScoreRecord()
        {
            if (Score > _gameData.Data.MaxScore)
            {
                _gameData.Data.MaxScore = Score;
                _gameData.Save();
            }
        }
    }
}