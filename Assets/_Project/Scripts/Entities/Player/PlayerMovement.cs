using GameScene.Interfaces;
using Zenject;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private IMovement _iMovement;
        
        [field: SerializeField] public float SpeedRotation { get; private set; }
        [field: SerializeField] public float SpeedMove { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }

        [Inject]
        public void Construct(IMovement iMovement)
        {
            _iMovement = iMovement;
        }
        
        private void Update()
        {
            _iMovement.Move(this);
        }
    }    
}

