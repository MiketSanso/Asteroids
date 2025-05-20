using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GameScene.Game
{
    public class EndPanelView : MonoBehaviour
    {
        public Button ButtonInterstitialAds;
        public Button ButtonRewardedAds;
        public Button ButtonGoMenu;
        public GameObject _panel;
        public TMP_Text _text;
        
        public void Deactivate()
        {
            _panel.SetActive(false);
        }

        public void Activate()
        {
            _panel.SetActive(true);
        }

        public void UpdateScoreDisplay(float score)
        {
            _text.text = score.ToString();
        }
    }
}