using System;
using GameScene.Common;
using GameScene.Common.DataSaveSystem;
using GameScene.Game;
using UnityEngine;

namespace GameScene.Models
{
    public class ScorePresenter : IDisposable
    {
        private readonly ScoreModel _model;
        private readonly GameEventBus _gameEventBus;
        private readonly DataPresenter _saveDataService;
        
        private EndPanelView _view;

        public ScorePresenter(ScoreModel model, 
            GameEventBus gameEventBus,
            DataPresenter gameData)
        {
            _gameEventBus = gameEventBus;
            _saveDataService = gameData;
            _model = model;
        }

        public void Dispose()
        {
            _gameEventBus.OnRestart -= ResetScore;
            _gameEventBus.OnFinish -= CheckScoreRecord;
        }
        
        public void Initialize(EndPanelView scoreView)
        {
            _gameEventBus.OnRestart += ResetScore;
            _gameEventBus.OnFinish += CheckScoreRecord;
            _view = scoreView;
        }

        public void AddScore(int value, Transform transform)
        {
            _model.AddScore(value);
            _view.UpdateScoreDisplay(_model.Score);
        }

        private void ResetScore()
        {
            _model.ResetScore();
            _view?.UpdateScoreDisplay(_model.Score);
        }
        
        private void CheckScoreRecord()
        {
            if (_model.Score > _saveDataService.GetMaxScore())
            {
                _saveDataService.ChangeMaxScore(_model.Score);
                _saveDataService.Save();
            }
        }
    }
}