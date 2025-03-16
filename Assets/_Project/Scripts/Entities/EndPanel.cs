using GameScene.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        
        private Player _player;

        private void OnDisable()
        {
            _player.OnDeath -= Activate;
        }
        
        public void Initialize(Player player)
        {
            _player = player;
            _player.OnDeath += Activate;
        }

        private void Activate()
        {
            _panel.SetActive(true);
        }

        private void Deactivate()
        {
            _panel.SetActive(false);
        }
    }
}