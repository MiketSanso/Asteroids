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
        public GameObject Panel;
        public TMP_Text Text;
        
        public void Deactivate()
        {
            Panel.SetActive(false);
        }

        public void Activate()
        {
            Panel.SetActive(true);
        }

        public void UpdateScoreDisplay(float score)
        {
            Text.text = score.ToString();
        }
    }
}