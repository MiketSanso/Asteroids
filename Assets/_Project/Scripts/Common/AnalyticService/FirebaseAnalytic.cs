using Firebase.Analytics;
using GameScene.Entities.PlayerSpace;
using GameScene.Factories;

namespace GameScene.Common
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
        private readonly GameEventBus _gameEventBus;

        public FirebaseAnalytic(GameEventBus gameEventBus)
        {
            _gameEventBus = gameEventBus;
        }

        public void Initialize()
        {
            _gameEventBus.OnStart += Start;
            _gameEventBus.OnFinish += EndGame;
        }

        public void Dispose()
        {
            _gameEventBus.OnStart -= Start;
            _gameEventBus.OnFinish -= EndGame;
        }
        
        public void AddBulletShot() => _countBulletShots++;
        
        public void AddDestroyedAsteroid() => _countBulletShots++;
        
        public void AddDestroyedUfo() => _countBulletShots++;
    
        public void UseLaser()
        {
            _countBulletShots++;
            FirebaseAnalytics.LogEvent("use_laser");
        }
        
        private void Start()
        {
            FirebaseAnalytics.LogEvent("start_game");
        }

        private void EndGame()
        {
            FirebaseAnalytics.LogEvent("end_game",
                new Parameter("count_bullet_shots", _countBulletShots),
                new Parameter("count_laser_shots", _countLaserShots),
                new Parameter("count_destroyed_asteroids", _countDestroyedAsteroids),
                new Parameter("count_destroyed_ufo", _countDestroyedUfo)
            );
        }
    }
}
