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
            _gameStateController.OnFinish += Activate;
            _gameStateController.OnRestart += Deactivate;
            Deactivate();
        }
        
        private void OnDestroy()
        {
            _gameStateController.OnFinish -= Activate;
            _gameStateController.OnRestart -= Deactivate;
        }
        
        public void Deactivate()
        {
            _panel.SetActive(false);
        }

        private void Activate()
        {
            _panel.SetActive(true);
            _text.text = _scoreRepository.Score.ToString();
        }
    }
}