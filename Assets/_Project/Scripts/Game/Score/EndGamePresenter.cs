using System;
using GameScene.Game;

namespace GameScene.Models
{
    public class EndGamePresenter : IDisposable
    {
        private readonly ScoreService _scoreService;
        private EndPanelView _view;

        public EndGamePresenter(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Initialize(EndPanelView view)
        {
            _view = view;
            _scoreService.OnScoreChange += UpdateScoreDisplay;
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            _view?.UpdateScoreDisplay(_scoreService.CurrentScore);
        }

        public void Dispose()
        {
            _scoreService.OnScoreChange -= UpdateScoreDisplay;
        }
    }
}