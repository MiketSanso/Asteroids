using GameScene.Entities.Player;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameScene.Repositories;
using TMPro;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour
    {
        public event Action OnRestart;
        
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _text;
        
        private ScoreInfo _scoreInfo;
        private PlayerUI _playerUI;
        
        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
        }
        
        private void OnDestroy()
        {
            _playerUI.OnDeath -= Activate;
            _restartButton.onClick.RemoveListener(Restart);
        }
        
        private void Restart()
        {
            OnRestart?.Invoke();
            Deactivate();
        }
        
        public void Initialize(PlayerUI playerUI, ScoreInfo scoreInfo)
        {
            _scoreInfo = scoreInfo;
            _playerUI = playerUI;
            _playerUI.OnDeath += Activate;
        }

        private void Activate()
        {
            _panel.SetActive(true);
            _text.text = _scoreInfo.Score.ToString();
        }

        private void Deactivate()
        {
            _panel.SetActive(false);
        }
    }
}