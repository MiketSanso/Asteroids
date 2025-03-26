using GameScene.Interfaces;
using Zenject;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IMovement _movement;
        
        [field: SerializeField] public float SpeedRotation { get; private set; }
        [field: SerializeField] public float SpeedMove { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }

        [Inject]
        private void Construct(IMovement movement)
        {
            _movement = movement;
        }
        
        private void Update()
        {
            _movement.Move(this);
        }
    }    
}

