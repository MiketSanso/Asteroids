using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _button;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null;
 
    private void Start()
    {
        _adUnitId = _androidAdUnitId;
        _button.interactable = false;
        Advertisement.Load(_adUnitId, this);
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

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    { }

    public void ShowAd()
    {
        _button.interactable = false;
        Advertisement.Show(_adUnitId, this);
        Advertisement.Load(_adUnitId, this);
    }
 
    void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
}