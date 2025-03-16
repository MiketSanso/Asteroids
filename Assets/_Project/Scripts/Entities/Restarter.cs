using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level.UFO
{
    public class Restarter : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        
        

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
            
        }
    }
}