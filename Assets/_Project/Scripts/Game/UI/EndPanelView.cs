using GameScene.Common;
using UnityEngine;
using GameScene.Models;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace GameScene.Game
{
    public class EndPanelView : MonoBehaviour
    {
        [SerializeField] private Button _buttonInterstitialAds;
        [SerializeField] private Button _buttonRewardedAds;
        [SerializeField] private Button _buttonGoMenu;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;
        
        private ScorePresenter _scoreModel;
        private GameEventBus _gameEventBus;
        private IAdsService _adsService;
        
        [Inject]
        private void Construct(GameEventBus gameEventBus, 
            ScorePresenter scoreModel, 
            IAdsService adsService)
        {
            _gameEventBus = gameEventBus;
            _scoreModel = scoreModel;
            _adsService = adsService;
        }
        
        private void Start()
        {
            _buttonGoMenu.onClick.AddListener(ActivateMenu);
            _buttonInterstitialAds.onClick.AddListener(_adsService.ShowInterstitialAds);
            _buttonRewardedAds.onClick.AddListener(_adsService.ShowRewardedAds);

            _gameEventBus.OnResume += Deactivate;
            _gameEventBus.OnFinish += Activate;
            _gameEventBus.OnRestart += Deactivate;
            Deactivate();
        }
        
        private void OnDestroy()
        {
            _buttonGoMenu.onClick.RemoveListener(ActivateMenu);
            _buttonInterstitialAds.onClick.RemoveListener(_adsService.ShowInterstitialAds);
            _buttonRewardedAds.onClick.RemoveListener(_adsService.ShowRewardedAds);
            
            _gameEventBus.OnResume -= Deactivate;
            _gameEventBus.OnFinish -= Activate;
            _gameEventBus.OnRestart -= Deactivate;
        }

        private void ActivateMenu()
        {
            SceneManager.LoadScene(1);
        }
        
        private void Deactivate()
        {
            _panel.SetActive(false);
        }

        private void Activate()
        {
            _panel.SetActive(true);
        }

        public void UpdateScoreDisplay(float score)
        {
            _text.text = score.ToString();
        }
    }
}