using GameScene.Common;
using GameScene.Common.ChangeSceneService;
using UnityEngine;
using TMPro;
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
        
        private GameEventBus _gameEventBus;
        private IAdsService _adsService;
        private SceneChanger _sceneChanger;
        
        [Inject]
        private void Construct(GameEventBus gameEventBus,
            IAdsService adsService,
            SceneChanger sceneChanger)
        {
            _gameEventBus = gameEventBus;
            _adsService = adsService;
            _sceneChanger = sceneChanger;
        }
        
        private void Start()
        {
            _buttonGoMenu.onClick.AddListener(_sceneChanger.ActivateMenu);
            _buttonInterstitialAds.onClick.AddListener(_adsService.ShowInterstitialAds);
            _buttonRewardedAds.onClick.AddListener(_adsService.ShowRewardedAds);

            _gameEventBus.OnResume += Deactivate;
            _gameEventBus.OnFinish += Activate;
            _gameEventBus.OnRestart += Deactivate;
            Deactivate();
        }
        
        private void OnDestroy()
        {
            _buttonGoMenu.onClick.RemoveListener(_sceneChanger.ActivateMenu);
            _buttonInterstitialAds.onClick.RemoveListener(_adsService.ShowInterstitialAds);
            _buttonRewardedAds.onClick.RemoveListener(_adsService.ShowRewardedAds);
            
            _gameEventBus.OnResume -= Deactivate;
            _gameEventBus.OnFinish -= Activate;
            _gameEventBus.OnRestart -= Deactivate;
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