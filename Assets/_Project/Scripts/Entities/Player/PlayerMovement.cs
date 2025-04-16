using GameScene.Interfaces;
using Zenject;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IInputService _inputService;
        
        [field: SerializeField] public float SpeedRotation { get; private set; }
        [field: SerializeField] public float SpeedMove { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Update()
        {
            _inputService.Move(this);
        }
    }    
}

