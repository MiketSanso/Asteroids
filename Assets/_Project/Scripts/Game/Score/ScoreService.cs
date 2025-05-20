using System;
using GameScene.Common;
using UnityEngine;
using Zenject;

namespace GameScene.Models
{
    public class ScoreService : IInitializable, IDisposable
    {
        public event Action OnScoreChange;
    
        private readonly ScoreModel _model;
        private readonly GameEventBus _eventBus;
        private readonly DataService _dataService;
        private readonly DataPresenter _dataPresenter;
    
        public float CurrentScore => _model.Score;
    
        public ScoreService(ScoreModel model,
            GameEventBus eventBus,
            DataService dataService,
            DataPresenter dataPresenter)
        {
            _model = model;
            _eventBus = eventBus;
            _dataService = dataService;
            _dataPresenter = dataPresenter;
        }
    
        public void Initialize()
        {
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
            if (_model.Score > _dataPresenter.GetMaxScore())
            {
                _dataService.SetMaxScore(_model.Score);
                _dataService.Save();
            }
        }
    
        public void Dispose()
        {
            _eventBus.OnRestart -= ResetScore;
            _eventBus.OnFinish -= CheckScoreRecord;
        }
    }
}