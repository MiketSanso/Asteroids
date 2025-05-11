using System;
using Cysharp.Threading.Tasks;
using GameScene.Common;
using GameScene.Common.DataSaveSystem;
using UnityEngine;
using Zenject;

namespace GameScene.Repositories
{
    public class ScoreRepository : IInitializable, IDisposable
    {
        private readonly GameStateController _gameStateController;
        private readonly SaveService _saveDataService;
        
        public float Score { get; private set; }
        
        public ScoreRepository(GameStateController gameStateController,
            SaveService gameData)
        {
            _gameStateController = gameStateController;
            _saveDataService = gameData;
        }

        public void Initialize()
        {
            SubscribeForObjects().Forget();
        }

        public void Dispose()
        {
            _gameStateController.OnRestart -= ResetScore;
            _gameStateController.OnFinish -= CheckScoreRecord;
        }
        
        public void AddScore(int scoreSize, Transform transform)
        {
            Score += scoreSize;
        }
        
        private async UniTask SubscribeForObjects()
        {
            _gameStateController.OnRestart += ResetScore;
            _gameStateController.OnFinish += CheckScoreRecord;
        }

        private void ResetScore()
        {
            Score = 0;
        }
        
        private void CheckScoreRecord()
        {
            if (Score > _saveDataService.Data.MaxScore)
            {
                _saveDataService.Data.MaxScore = Score;
                _saveDataService.Save().Forget();
            }
        }
    }
}