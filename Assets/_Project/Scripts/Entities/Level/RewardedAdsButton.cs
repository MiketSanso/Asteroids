using GameScene.Entities.Player;
using GameScene.Factories;
using GameScene.Level;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Zenject;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string ANDROID_AD_UNIT_ID = "Rewarded_Android";
    
    [SerializeField] Button _button;
    [SerializeField] private EndPanel _endPanel;
    
    private GameStateController _gameStateController;
    private PlayerUI _playerUI;
    private AsteroidFactory _asteroidFactory;
    private string _adUnitId = null;

    [Inject]
    private void Construct(PlayerUI playerUI, AsteroidFactory asteroidFactory, GameStateController gameStateController)
    {
        _playerUI = playerUI;
        _asteroidFactory = asteroidFactory;
        _gameStateController = gameStateController;
    }
    
    private void Start()
    {
        _adUnitId = ANDROID_AD_UNIT_ID;
        _button.interactable = false;
        _gameStateController.OnRestart += LoadAds;

        Advertisement.Load(_adUnitId, this);
    }
    
    private void OnDestroy()
    {
        _gameStateController.OnRestart -= LoadAds;
        _button.onClick.RemoveAllListeners();
    }
    
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(_adUnitId))
        {
            _button.onClick.AddListener(ShowAd);
            _button.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        _playerUI.Activate();
        _asteroidFactory.RestartFly();
        _endPanel.Deactivate();
    }
    
    private void ShowAd()
    {
        _button.interactable = false;
        Advertisement.Show(_adUnitId, this);
    }

    private void LoadAds()
    {
        Advertisement.Load(_adUnitId, this);
    }
}