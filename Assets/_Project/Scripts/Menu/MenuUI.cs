using System;
using Cysharp.Threading.Tasks;
using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Common.DataSaveSystem;
using GameScene.Repositories.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        
        private ConfigLoadService _configLoadService;
        private AnimateShopConfig _animateShopData;  
        private IBuyNoAdsService _buyNoAdsService;
        private SaveService _saveService;
        
        [Inject]
        private void Construct(IBuyNoAdsService buyNoAdsService, SaveService saveService, ConfigLoadService configLoadService)
        {
            _saveService = saveService;
            _buyNoAdsService = buyNoAdsService;
            _configLoadService = configLoadService;
        }
        
        private void Start()
        {
            Initialize();
            
            _buttonNoAds.onClick.AddListener(_buyNoAdsService.BuyNoAds);
            _buttonExit.onClick.AddListener(Exit);
            _buttonStartGame.onClick.AddListener(StartGame);
            
            UpdateUI();
            _buyNoAdsService.OnDisableAds += UpdateUI;
            _buyNoAdsService.OnSendInfo += ChangeText;
        }
    
        private void OnDestroy()
        {
            _buttonNoAds.onClick.RemoveListener(_buyNoAdsService.BuyNoAds);
            _buttonExit.onClick.RemoveListener(Exit);
            _buttonStartGame.onClick.RemoveListener(StartGame);
            
            _buyNoAdsService.OnDisableAds -= UpdateUI;
            _buyNoAdsService.OnSendInfo -= ChangeText;
        }

        private async void Initialize()
        {
            _animateShopData = await _configLoadService.Load<AnimateShopConfig>(SHOP_CONFIG);
        }
        
        private void StartGame()
        {
            SceneManager.LoadScene(2);
        }
        
        private void Exit()
        {
            Application.Quit();
        }
        
        private void UpdateUI()
        {
            _buttonNoAds.interactable = !_saveService.Data.IsAdsOff;
        }

        private void ChangeText(string textMessage)
        {
            UpdateTextInfo(textMessage).Forget();
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
