using UnityEngine;
using UnityEngine.UI;
using GameScene.Repositories;
using TMPro;
using Zenject;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _text;
        
        private ScoreRepository _scoreRepository;
        private GameStateController _gameStateController;
        
        [Inject]
        private void Construct(GameStateController gameStateController, ScoreRepository scoreRepository)
        {
            _gameStateController = gameStateController;
            _scoreRepository = scoreRepository;
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
        
        public void Deactivate()
        {
            _panel.SetActive(false);
        }
        
        private void Restart()
        {
            _gameStateController.RestartGame();
            Deactivate();
        }

        private void Activate()
        {
            _panel.SetActive(true);
            _text.text = _scoreRepository.Score.ToString();
        }
    }
}