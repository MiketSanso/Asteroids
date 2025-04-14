using Cysharp.Threading.Tasks;
using GameScene.Entities.Asteroid;
using GameScene.Entities.UFOs;
using GameScene.Level;
using UnityEngine;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreRepository : IInitializable
    {
        private readonly GameStateController _gameStateController;
        private readonly GameData _gameData;
        
        public float Score { get; private set; }
        
        public ScoreRepository(GameStateController gameStateController,
        GameData gameData)
        {
            _gameStateController = gameStateController;
            _gameData = gameData;
        }

        public void Initialize()
        {
            SubscribeForObjects().Forget();
        }

        private async UniTask SubscribeForObjects()
        {
            _gameStateController.OnRestart += ResetScore;
            _gameStateController.OnFinish += CheckScoreRecord;
            _gameStateController.OnCloseGame += Destroy;
        }
        
        private void Destroy()
        {
            _gameStateController.OnRestart -= ResetScore;
            _gameStateController.OnFinish -= CheckScoreRecord;
            _gameStateController.OnCloseGame -= Destroy;
        }
        
        public void AddScore(int scoreSize, Transform transform)
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