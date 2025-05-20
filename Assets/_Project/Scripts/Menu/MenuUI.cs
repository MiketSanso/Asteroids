using System;
using Cysharp.Threading.Tasks;
using GameScene.Common;
using GameScene.Common.ChangeSceneService;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Models.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace  GameScene.Menu
{
    public class MenuUI : MonoBehaviour
    {
        private const string SHOP_CONFIG = "AnimateShopConfig";
        
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private Button _buttonNoAds;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonStartGame;
        
        private IConfigLoadService _configLoadService;
        private AnimateShopConfig _animateShopData;  
        private IBuyNoAdsService _buyNoAdsService;
        private ReportTextsData _reportTextsData;
        private SceneChanger _sceneChanger;
        
        [Inject]
        private void Construct(IBuyNoAdsService buyNoAdsService, 
            IConfigLoadService configLoadService,
            ReportTextsData reportTextsData,
            SceneChanger sceneChanger)
        {
            _buyNoAdsService = buyNoAdsService;
            _configLoadService = configLoadService;
            _reportTextsData = reportTextsData;
            _sceneChanger = sceneChanger;
        }
        
        private void Start()
        {
            Initialize();
            
            _buttonNoAds.onClick.AddListener(_buyNoAdsService.BuyNoAds);
            _buttonExit.onClick.AddListener(_sceneChanger.CloseGame);
            _buttonStartGame.onClick.AddListener(_sceneChanger.ActivateGameScene);
            
            _buyNoAdsService.OnBuySuccess += SetSuccessText;
            _buyNoAdsService.OnBuyUnavailable += SetUnavailableText;
            _buyNoAdsService.OnBuyFailed += SetFailedText;

        }
    
        private void OnDestroy()
        {
            _buttonNoAds.onClick.RemoveListener(_buyNoAdsService.BuyNoAds);
            _buttonExit.onClick.RemoveListener(_sceneChanger.CloseGame);
            _buttonStartGame.onClick.RemoveListener(_sceneChanger.ActivateGameScene);

            _buyNoAdsService.OnBuySuccess -= SetSuccessText;
            _buyNoAdsService.OnBuyUnavailable -= SetUnavailableText;
            _buyNoAdsService.OnBuyFailed -= SetFailedText;
        }

        private async void Initialize()
        {
            _animateShopData = await _configLoadService.Load<AnimateShopConfig>(SHOP_CONFIG);
        }

        private async void SetFailedText()
        {
            await UpdateTextInfo(_reportTextsData.TextFailed);
        }
        
        private async void SetSuccessText()
        {
            await UpdateTextInfo(_reportTextsData.TextSuccess);
        }
        
        private async void SetUnavailableText()
        {
            await UpdateTextInfo(_reportTextsData.TextUnavailable);
        }

        private async UniTask UpdateTextInfo(string textMessage)
        {
            _statusText.text = textMessage;
            _statusText.alpha = 1;

            while (_statusText.alpha != 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_animateShopData.TimeStep));
                _statusText.alpha -= _animateShopData.TimeStep;
            }
        }
    }
}
