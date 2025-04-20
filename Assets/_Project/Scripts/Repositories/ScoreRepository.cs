using _Project.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;
using GameScene.Level;
using UnityEngine;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreRepository : IInitializable
    {
        private readonly GameStateController _gameStateController;
        private readonly SaveService _saveDataSevice;
        
        public float Score { get; private set; }
        
        public ScoreRepository(GameStateController gameStateController,
            SaveService gameData)
        {
            _gameStateController = gameStateController;
            _saveDataSevice = gameData;
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
            if (Score > _saveDataSevice.Data.MaxScore)
            {
                _saveDataSevice.Data.MaxScore = Score;
                _saveDataSevice.Save();
            }
        }
    }
}