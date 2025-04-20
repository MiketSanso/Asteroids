using System;
using Firebase.Analytics;
using GameScene.Entities.Player;
using GameScene.Factories;
using GameScene.Interfaces;
using GameScene.Level;
using UnityEngine;
using Zenject;

namespace GameSystem
{
    public class FirebaseAnalytic : IAnalyticService
    {
        private int _countBulletShots;
        private int _countLaserShots;
        private int _countDestroyedAsteroids;
        private int _countDestroyedUfo;
        
        private readonly Laser _laser;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly UfoFactory _ufoFactory;
        private readonly GameStateController _gameStateController;

        public FirebaseAnalytic(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        public void Initialize()
        {
            _gameStateController.OnStartGame += StartGame;
            _gameStateController.OnFinish += EndGame;
        }

        public void Dispose()
        {
            _gameStateController.OnStartGame -= StartGame;
            _gameStateController.OnFinish -= EndGame;
        }
        
        public void AddBulletShot() => _countBulletShots++;
        
        public void AddDestroyedAsteroid() => _countBulletShots++;
        
        public void AddDestroyedUfo() => _countBulletShots++;
        
        public void StartGame()
        {
            FirebaseAnalytics.LogEvent("start_game");
        }

        public void EndGame()
        {
            FirebaseAnalytics.LogEvent("end_game",
                new Parameter("count_bullet_shots", _countBulletShots),
                new Parameter("count_laser_shots", _countLaserShots),
                new Parameter("count_destroyed_asteroids", _countDestroyedAsteroids),
                new Parameter("count_destroyed_ufo", _countDestroyedUfo)
            );
        }
    
        public void UseLaser()
        {
            _countBulletShots++;
            FirebaseAnalytics.LogEvent("use_laser");
        }
    }
}
