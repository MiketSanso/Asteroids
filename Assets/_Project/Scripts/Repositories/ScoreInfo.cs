using GameScene.Entities.Asteroid;
using GameScene.Entities.UFOs;
using UnityEngine;
using GameScene.Level;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreInfo : MonoBehaviour
    {
        private PoolObjects _poolObjects;
        [Inject] private EndPanel _endPanel;
        
        public float Score { get; private set; }
        
        private void OnDestroy()
        {
            _endPanel.OnRestart -= ResetScore;
            
            foreach (AsteroidUI asteroid in _poolObjects.Asteroids)
            {
                asteroid.OnDestroyedWithScore -= AddScore;
            }
            
            foreach (AsteroidUI asteroid in _poolObjects.SmallAsteroids)
            {
                asteroid.OnDestroyedWithScore -= AddScore;
            }
            
            foreach (Ufo ufo in _poolObjects.Ufos)
            {
                ufo.OnDestroyed -= AddScore;
            }
        }
        
        public void Initialize(PoolObjects poolObjects)
        {
            _poolObjects = poolObjects;
            _endPanel.OnRestart += ResetScore;
            
            foreach (AsteroidUI asteroid in _poolObjects.Asteroids)
            {
                asteroid.OnDestroyedWithScore += AddScore;
            }
            
            foreach (AsteroidUI asteroid in _poolObjects.SmallAsteroids)
            {
                asteroid.OnDestroyedWithScore += AddScore;
            }
            
            foreach (Ufo ufo in _poolObjects.Ufos)
            {
                ufo.OnDestroyed += AddScore;
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