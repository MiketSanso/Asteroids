using GameScene.Common;
using UnityEngine;
using GameScene.Repositories;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace GameScene.Game
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private Button _buttonInterstitialAds;
        [SerializeField] private Button _buttonRewardedAds;
        [SerializeField] private Button _buttonGoMenu;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;
        
        private ScoreRepository _scoreRepository;
        private GameStateController _gameStateController;
        private IRewardedAdsService _rewardedAdsService;
        private IInterstitialAdsService _interstitialAdsService;
        
        [Inject]
        private void Construct(GameStateController gameStateController, 
            ScoreRepository scoreRepository, 
            IRewardedAdsService rewardedAdsService,
            IInterstitialAdsService interstitialAdsService)
        {
            _gameStateController = gameStateController;
            _scoreRepository = scoreRepository;
            _rewardedAdsService = rewardedAdsService;
            _interstitialAdsService = interstitialAdsService;
        }
        
        private void Start()
        {
            _buttonGoMenu.onClick.AddListener(ActivateMenu);
            _buttonInterstitialAds.onClick.AddListener(_interstitialAdsService.ShowAds);
            _buttonRewardedAds.onClick.AddListener(_rewardedAdsService.ShowAds);

            _gameStateController.OnResume += Deactivate;
            _gameStateController.OnFinish += Activate;
            _gameStateController.OnRestart += Deactivate;
            Deactivate();
        }
        
        private void OnDestroy()
        {
            _buttonGoMenu.onClick.RemoveListener(ActivateMenu);
            _buttonInterstitialAds.onClick.RemoveListener(_interstitialAdsService.ShowAds);
            _buttonRewardedAds.onClick.RemoveListener(_rewardedAdsService.ShowAds);
            
            _gameStateController.OnResume -= Deactivate;
            _gameStateController.OnFinish -= Activate;
            _gameStateController.OnRestart -= Deactivate;
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
            _text.text = _scoreRepository.Score.ToString();
        }
    }
}