using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.Player
{
    public class KeyboardMovement : MonoBehaviour, IMovement
    {
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
        
        public void Move()
        {
            if (Input.GetButton("Horizontal"))
            {
                _playerMovement.transform.Rotate(0, 0, _playerMovement.SpeedRotation * -Input.GetAxis("Horizontal"));
            }

            if (Input.GetKey(KeyCode.W))
            {
                MoveForward();
            }
        }
        
        private void MoveForward()
        {
            float angle = (transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            _playerMovement.Rb.AddForce(direction.normalized * _playerMovement.SpeedMove);
            _playerMovement.Rb.linearVelocity = Vector2.ClampMagnitude(_playerMovement.Rb.linearVelocity, _playerMovement.MaxSpeed);
        }
    }
}