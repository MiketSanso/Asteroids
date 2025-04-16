using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform _transformSpawn;
        
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            _inputService.Shot(_transformSpawn);
        }
    }
}