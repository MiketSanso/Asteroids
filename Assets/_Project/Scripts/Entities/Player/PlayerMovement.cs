using GameScene.Interfaces;
using Zenject;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IInputSystem _inputSystem;
        
        [field: SerializeField] public float SpeedRotation { get; private set; }
        [field: SerializeField] public float SpeedMove { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }

        [Inject]
        private void Construct(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }
        
        private void Update()
        {
            _inputSystem.Move(this);
        }
    }    
}

