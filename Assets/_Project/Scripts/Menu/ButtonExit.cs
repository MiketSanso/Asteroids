using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Menu
{
    public class ButtonExit : MonoBehaviour
    {
        public Button _button;

        private void Start()
        {
            _button.onClick.AddListener(Exit);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Exit);
        }
        
        private void Exit()
        {
            Application.Quit();
        }
    }
}