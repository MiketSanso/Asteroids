using System;
using GameScene.Common;
using UnityEngine;
using Zenject;

namespace GameScene.Models
{
    public class ScoreController : IInitializable, IDisposable
    {
        public event Action OnScoreChange;
    
        private readonly ScoreModel _model;
        private readonly GameStateController _stateController;
        private readonly DataController _dataController;
        private readonly GameEndController _gameEndController;
    
        public float CurrentScore => _model.Score;
    
        public ScoreController(ScoreModel model,
            GameStateController stateController,
            GameEndController gameEndController,
            DataController dataController)
        {
            _model = model;
            _stateController = stateController;
            _dataController = dataController;
            _gameEndController = gameEndController;
        }
    
        public void Initialize()
        {
            _gameEndController.OnRestart += ResetScore;
            _stateController.OnFinish += CheckScoreRecord;
        }
        
        public void Dispose()
        {
            _gameEndController.OnRestart -= ResetScore;
            _stateController.OnFinish -= CheckScoreRecord;
        }
    
        public void AddScore(int value, Transform transform)
        {
            _model.AddScore(value);
            OnScoreChange?.Invoke();
        }
    
        private void ResetScore()
        {
            _model.ResetScore();
            OnScoreChange?.Invoke();
        }
    
        private void CheckScoreRecord()
        {
            if (_model.Score > _dataController.GetMaxScore())
            {
                _dataController.SetMaxScore(_model.Score);
                _dataController.Save();
            }
        }
    }
}