using UnityEngine;
using GameScene.Interfaces;
using GameScene.Level;
using Zenject;

namespace GameScene.Entities.Player
{
    public class PlayerUI : MonoBehaviour
    {
        private GameStateController _gameStateController;

        [Inject]
        private void Construct(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }
        
        private void Start()
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
        
        private void Deactivate()
        {
            _gameStateController.FinishGame();
            gameObject.SetActive(false);
        }

        private void Activate()
        {
            gameObject.SetActive(true);
            transform.position = Vector3.zero;
        }
    }
}