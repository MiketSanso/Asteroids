using GameScene.Common;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.PlayerSpace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _transformSpawn;
        
        private GameEventBus _gameEventBus;
        private PlayerController _playerController;
        private IInstantiator _instantiator;

        [Inject]
        private void Construct(GameEventBus gameEventBus, IInstantiator instantiator)
        {
            _gameEventBus = gameEventBus;
            _instantiator = instantiator;
        }
        
        private void Start()
        {
            _playerController = _instantiator.Instantiate<PlayerController>(); 
            _playerController.Initialize(_rb, this, _transformSpawn);
            _gameEventBus.OnRestart += Activate;
            _gameEventBus.OnResume += Activate;
        }
        
        private void OnDestroy()
        {
            _playerController.Destroy();
            _gameEventBus.OnResume -= Activate;
            _gameEventBus.OnRestart -= Activate;
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
            OnPlayerDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }
}