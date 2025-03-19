using GameScene.Entities.Asteroid;
using GameScene.Entities.UFOs;
using GameScene.Level;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreInfo
    {
        private AsteroidsPool _poolAsteroids;
        private UfoPool _poolUfo;
        private EndPanel _endPanel;
        
        public float Score { get; private set; }
        
        [Inject]
        public void Construct(EndPanel endPanel)
        {
            _endPanel = endPanel;
        }
        
        public void Initialize(AsteroidsPool poolAsteroids,
        UfoPool poolUfo)
        {
            _poolAsteroids = poolAsteroids;
            _poolUfo = poolUfo;
            
            foreach (AsteroidUI asteroid in _poolAsteroids.Asteroids)
            {
                asteroid.OnDestroyedWithScore += AddScore;
            }
            
            foreach (AsteroidUI asteroid in _poolAsteroids.SmallAsteroids)
            {
                asteroid.OnDestroyedWithScore += AddScore;
            }
            
            foreach (Ufo ufo in _poolUfo.Ufos)
            {
                ufo.OnDestroyed += AddScore;
            }
        }
        
        private void Destroy()
        {
            _endPanel.OnRestart -= ResetScore;
            
            foreach (AsteroidUI asteroid in _poolAsteroids.Asteroids)
            {
                asteroid.OnDestroyedWithScore -= AddScore;
            }
            
            foreach (AsteroidUI asteroid in _poolAsteroids.SmallAsteroids)
            {
                asteroid.OnDestroyedWithScore -= AddScore;
            }
            
            foreach (Ufo ufo in _poolUfo.Ufos)
            {
                ufo.OnDestroyed -= AddScore;
            }
        }
        
        private void AddScore(int score)
        {
            Score += score;
        }

        private void ResetScore()
        {
            Score = 0;
        }
    }
}