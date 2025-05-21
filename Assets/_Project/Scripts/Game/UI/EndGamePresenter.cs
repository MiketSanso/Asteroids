using System;
using GameScene.Common;
using GameScene.Common.ChangeSceneService;
using GameScene.Game;

namespace GameScene.Models
{
    public class EndGamePresenter : IDisposable
    {
        private EndPanelView _endPanelView;
        
        private readonly ScoreController _scoreController;
        private readonly GameStateController _gameStateController;
        private readonly IAdsService _adsService;
        private readonly SceneChanger _sceneChanger;
        private readonly GameEndController _gameEndController;
        
        public EndGamePresenter(ScoreController scoreController,
            GameEndController gameEndController,
            IAdsService adsService,
            SceneChanger sceneChanger,
            GameStateController gameStateController)
        {
            _gameEndController = gameEndController;
            _gameStateController = gameStateController;
            _adsService = adsService;
            _sceneChanger = sceneChanger;
            _scoreController = scoreController;
        }

        public void Initialize(EndPanelView endPanelView)
        {
            _endPanelView = endPanelView;

            _endPanelView.ButtonGoMenu.onClick.AddListener(_sceneChanger.ActivateMenu);
            _endPanelView.ButtonInterstitialAds.onClick.AddListener(_adsService.ShowInterstitialAds);
            _endPanelView.ButtonRewardedAds.onClick.AddListener(_adsService.ShowRewardedAds);
            
            _gameEndController.OnResume += _endPanelView.Deactivate;
            _gameStateController.OnFinish += _endPanelView.Activate;
            _gameEndController.OnRestart += _endPanelView.Deactivate;
            _endPanelView.Deactivate();
            
            _endPanelView = endPanelView;
            _scoreController.OnScoreChange += UpdateScoreDisplay;
            UpdateScoreDisplay();
        }

        public void Dispose()
        {
            _endPanelView.ButtonGoMenu.onClick.RemoveListener(_sceneChanger.ActivateMenu);
            _endPanelView.ButtonInterstitialAds.onClick.RemoveListener(_adsService.ShowInterstitialAds);
            _endPanelView.ButtonRewardedAds.onClick.RemoveListener(_adsService.ShowRewardedAds);
            
            _gameEndController.OnResume -= _endPanelView.Deactivate;
            _gameStateController.OnFinish -= _endPanelView.Activate;
            _gameEndController.OnRestart -= _endPanelView.Deactivate;
            
            _scoreController.OnScoreChange -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay()
        {
            _endPanelView?.UpdateScoreDisplay(_scoreController.CurrentScore);
        }
    }
}