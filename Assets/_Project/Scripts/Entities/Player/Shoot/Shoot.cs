using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform _transformSpawn;
        
        private IInputSystem _inputSystem;

        [Inject]
        private void Construct(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        private void Update()
        {
            _inputSystem.Shot(_transformSpawn);
        }
    }
}