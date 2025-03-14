using UnityEngine;

namespace GameScene.Level
{
    public class TeleportedObject : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Teleporter teleporter))
            {
                Vector2 vectorTeleport = new Vector2(gameObject.transform.position.x * teleporter.Direction.x,
                    gameObject.transform.position.y * teleporter.Direction.y);
                gameObject.transform.position = vectorTeleport;
            }
        }
    }
}