using UnityEngine;

namespace GameScene.Entities.Player
{
    public class MovementController
    {
        public void Rotate(PlayerMovement playerMovement)
        {
            playerMovement.transform.Rotate(0, 0, playerMovement.SpeedRotation * -Input.GetAxis("Horizontal"));
        }
        
        public void Move(PlayerMovement playerMovement)
        {
            float angle = (playerMovement.transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            playerMovement.Rb.AddForce(direction.normalized * playerMovement.SpeedMove);
            playerMovement.Rb.linearVelocity = Vector2.ClampMagnitude(playerMovement.Rb.linearVelocity, playerMovement.MaxSpeed);
        }
    }
}