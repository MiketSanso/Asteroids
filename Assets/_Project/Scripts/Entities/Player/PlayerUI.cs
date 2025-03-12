using UnityEngine;
using GameScene.Level;

namespace GameScene.Entities.Player
{
    public class PlayerUI : MonoBehaviour
    {
        private readonly Player _player = new Player();
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Teleporter teleporter))
            {
                _player.Teleport(gameObject, teleporter.Direction);
            }
        }
    }
}