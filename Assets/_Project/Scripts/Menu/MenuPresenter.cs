using System;
using GameScene.Common;
using GameScene.Common.ChangeSceneService;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using Zenject;

namespace GameScene.Menu
{
    public class MenuPresenter : IInitializable, IDisposable
    {
        private const string SHOP_CONFIG = "AnimateShopConfig";
        
        private readonly IConfigLoadService _configLoadService;
        private readonly IBuyNoAdsService _buyNoAdsService;
        private readonly SceneChanger _sceneChanger;
        private readonly MenuUI _menuUI;
        
        public MenuPresenter(IBuyNoAdsService buyNoAdsService, 
            IConfigLoadService configLoadService,
            SceneChanger sceneChanger,
            MenuUI menuUI)
        {
            _buyNoAdsService = buyNoAdsService;
            _configLoadService = configLoadService;
            _sceneChanger = sceneChanger;
            _menuUI = menuUI;
        }
        
        public async void Initialize()
        {
            _menuUI.ButtonNoAds.onClick.AddListener(_buyNoAdsService.BuyNoAds);
            _menuUI.ButtonExit.onClick.AddListener(_sceneChanger.CloseGame);
            _menuUI.ButtonStartGame.onClick.AddListener(_sceneChanger.ActivateGameScene);
            
            _buyNoAdsService.OnBuySuccess += _menuUI.SetSuccessText;
            _buyNoAdsService.OnBuyUnavailable += _menuUI.SetUnavailableText;
            _buyNoAdsService.OnBuyFailed += _menuUI.SetFailedText;

            _menuUI.InitializeConfig(await _configLoadService.Load<AnimateShopConfig>(SHOP_CONFIG));
        }
    
        public void Dispose()
        {
            _menuUI.ButtonNoAds.onClick.RemoveListener(_buyNoAdsService.BuyNoAds);
            _menuUI.ButtonExit.onClick.RemoveListener(_sceneChanger.CloseGame);
            _menuUI.ButtonStartGame.onClick.RemoveListener(_sceneChanger.ActivateGameScene);

            _buyNoAdsService.OnBuySuccess -= _menuUI.SetSuccessText;
            _buyNoAdsService.OnBuyUnavailable -= _menuUI.SetUnavailableText;
            _buyNoAdsService.OnBuyFailed -= _menuUI.SetFailedText;
        }
    }
}