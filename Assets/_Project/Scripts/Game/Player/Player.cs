using GameScene.Common;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.PlayerSpace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _transformSpawn;
        
        private GameEndController _gameEndController;
        private GameStateController _gameStateController;
        private PlayerController _playerController;
        private IInstantiator _instantiator;

        [Inject]
        private void Construct(GameEndController gameEndController, IInstantiator instantiator, GameStateController gameStateController)
        {
            _gameEndController = gameEndController;
            _instantiator = instantiator;
            _gameStateController = gameStateController;
        }
        
        private void Start()
        {
            _playerController = _instantiator.Instantiate<PlayerController>(); 
            _playerController.Initialize(_rb, this, _transformSpawn);
            _gameEndController.OnRestart += Activate;
            _gameEndController.OnResume += Activate;
        }
        
        private void OnDestroy()
        {
            _playerController.Destroy();
            _gameEndController.OnResume -= Activate;
            _gameEndController.OnRestart -= Activate;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDestroyableEnemy enemy))
            {
                Deactivate();
            }
        }
        
        private void Activate()
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