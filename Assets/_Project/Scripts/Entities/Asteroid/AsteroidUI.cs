using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out AsteroidDestroyer destroyer))
            {

            }
        }
    }
}