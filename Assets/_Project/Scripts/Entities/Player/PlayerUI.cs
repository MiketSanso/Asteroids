using UnityEngine;
using GameScene.Interfaces;
using GameScene.Level;
using Zenject;

namespace GameScene.Entities.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _transformSpawn;
        
        private GameStateController _gameStateController;
        private IInputService _inputService;
        private PlayerController _playerController;
        private IInstantiator _instantiator;

        [Inject]
        private void Construct(GameStateController gameStateController, IInputService inputService, IInstantiator instantiator)
        {
            _gameStateController = gameStateController;
            _inputService = inputService;
            _instantiator = instantiator;
        }
        
        private void Start()
        {
            _playerController = _instantiator.Instantiate<PlayerController>(); 
            _playerController.Initialize(_rb, this, _transformSpawn);
            _gameStateController.OnRestart += Activate;
        }
        
        private void Update()
        {
            _inputService.Move();
            _inputService.Shot();
        }
        
        private void OnDestroy()
        {
            _playerController.Destroy();
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