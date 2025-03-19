using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private IMovement _movement;
        
        [field: SerializeField] public float SpeedRotation { get; private set; }
        [field: SerializeField] public float SpeedMove { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        
        private void Update()
        {
            _movement.Move();
        }
    }    
}

