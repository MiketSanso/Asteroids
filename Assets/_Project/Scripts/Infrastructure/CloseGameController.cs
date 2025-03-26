using UnityEngine;
using Zenject;

namespace GameScene.Level
{
    public class CloseGameController : MonoBehaviour
    {
        private GameStateController _gameStateController;
        
        [Inject]
        private void Construct(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }

        private void OnDestroy()
        {
            _gameStateController.CloseGame();
        }
    }
}