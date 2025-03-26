using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class KeyboardMovement : IMovement
    {
        public void Move(PlayerMovement playerMovement)
        {
            if (Input.GetButton("Horizontal"))
            {
                playerMovement.transform.Rotate(0, 0, playerMovement.SpeedRotation * -Input.GetAxis("Horizontal"));
            }

            if (Input.GetKey(KeyCode.W))
            {
                float angle = (playerMovement.transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                playerMovement.Rb.AddForce(direction.normalized * playerMovement.SpeedMove);
                playerMovement.Rb.linearVelocity = Vector2.ClampMagnitude(playerMovement.Rb.linearVelocity, playerMovement.MaxSpeed);
            }
        }
    }
}