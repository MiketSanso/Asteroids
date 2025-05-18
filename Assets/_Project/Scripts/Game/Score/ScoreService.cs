using System;
using GameScene.Common;
using GameScene.Common.DataSaveSystem;
using UnityEngine;

namespace GameScene.Models
{
    public class ScoreService : IDisposable
    {
        public event Action OnScoreChange;
        
        private readonly ScoreModel _model;
        private readonly GameEventBus _eventBus;
        private readonly DataPresenter _saveDataService;

        public float CurrentScore => _model.Score;

        public ScoreService(ScoreModel model, GameEventBus eventBus, DataPresenter saveDataService)
        {
            _model = model;
            _eventBus = eventBus;
            _saveDataService = saveDataService;

            _eventBus.OnRestart += ResetScore;
            _eventBus.OnFinish += CheckScoreRecord;
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
            if (_model.Score > _saveDataService.GetMaxScore())
            {
                _saveDataService.ChangeMaxScore(_model.Score);
                _saveDataService.Save();
            }
        }

        public void Dispose()
        {
            _eventBus.OnRestart -= ResetScore;
            _eventBus.OnFinish -= CheckScoreRecord;
        }
    }
}