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
        private readonly GameEventBus _gameEventBus;
        private readonly IAdsService _adsService;
        private readonly SceneChanger _sceneChanger;
        
        public EndGamePresenter(ScoreController scoreController,
            GameEventBus gameEventBus,
            IAdsService adsService,
            SceneChanger sceneChanger)
        {
            _gameEventBus = gameEventBus;
            _adsService = adsService;
            _sceneChanger = sceneChanger;
            _scoreController = scoreController;
        }

        public void Initialize(EndPanelView endPanelView)
        {
            _endPanelView.ButtonGoMenu.onClick.AddListener(_sceneChanger.ActivateMenu);
            _endPanelView.ButtonInterstitialAds.onClick.AddListener(_adsService.ShowInterstitialAds);
            _endPanelView.ButtonRewardedAds.onClick.AddListener(_adsService.ShowRewardedAds);

            _gameEventBus.OnResume += _endPanelView.Deactivate;
            _gameEventBus.OnFinish += _endPanelView.Activate;
            _gameEventBus.OnRestart += _endPanelView.Deactivate;
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
            
            _gameEventBus.OnResume -= _endPanelView.Deactivate;
            _gameEventBus.OnFinish -= _endPanelView.Activate;
            _gameEventBus.OnRestart -= _endPanelView.Deactivate;
            
            _scoreController.OnScoreChange -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay()
        {
            _endPanelView?.UpdateScoreDisplay(_scoreController.CurrentScore);
        }
    }
}