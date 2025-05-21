using GameScene.Entities.PlayerSpace;
using UnityEngine;

namespace GameScene.Common
{
    public abstract class IDestroyableEnemy : MonoBehaviour
    {
        public abstract void Destroy();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Destroy();
            }
        }
    }
}