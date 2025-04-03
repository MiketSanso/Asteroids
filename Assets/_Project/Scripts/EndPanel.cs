using UnityEngine;
using UnityEngine.UI;
using GameScene.Repositories;
using TMPro;
using Zenject;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _text;
        
        private ScoreInfo _scoreInfo;
        private GameStateController _gameStateController;
        
        [Inject]
        private void Construct(GameStateController gameStateController, ScoreInfo scoreInfo)
        {
            _gameStateController = gameStateController;
            _scoreInfo = scoreInfo;
        }

        public void Initialize()
        {
            _restartButton.onClick.AddListener(Restart);
            _gameStateController.OnFinish += Activate;
            Deactivate();
        }
        
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(Restart);
            _gameStateController.OnFinish -= Activate;
        }
        
        private void Restart()
        {
            _gameStateController.RestartGame();
            Deactivate();
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