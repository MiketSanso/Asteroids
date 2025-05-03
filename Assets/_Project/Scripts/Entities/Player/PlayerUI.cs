using UnityEngine;
using GameScene.Interfaces;
using GameScene.Level;
using GameSystem;
using Zenject;

namespace GameScene.Entities.Player
{
    public class PlayerUI : MonoBehaviour
    {
        private GameStateController _gameStateController;
        private LoadPrefab<Sprite> _loadSprite;

        [Inject]
        private void Construct(GameStateController gameStateController, LoadPrefab<Sprite> loadSprite)
        {
            _gameStateController = gameStateController;
            _loadSprite = loadSprite;
        }
        
        private async void Start()
        {
            _gameStateController.OnRestart += Activate;
        }
        
        private void OnDestroy()
        {
            _gameStateController.OnRestart -= Activate;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDestroyableEnemy enemy))
            {
                enemy.Destroy();
                Deactivate();
            }
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
            transform.position = Vector3.zero;
        }
        
        private void Deactivate()
        {
            _gameStateController.FinishGame();
            gameObject.SetActive(false);
        }
    }
}