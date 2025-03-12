using UnityEngine;

namespace GameScene.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speedRotation;
        [SerializeField] private float speedMove;
        [SerializeField] private Rigidbody2D rb;
        
        private void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                transform.Rotate(0, 0, speedRotation * -Input.GetAxis("Horizontal"));
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

            rb.AddForce(direction * speedMove);
        }
    }    
}

