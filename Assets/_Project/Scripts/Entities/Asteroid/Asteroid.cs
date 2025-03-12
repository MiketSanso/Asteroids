using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        public Asteroid Create(Transform transformSpawn, Transform transformParent)
        {
            Asteroid asteroid = Instantiate(this, transformSpawn, transformParent);
            asteroid.Deactivate();
            
            return asteroid;
        }
        
        private void Activate()
        {
            gameObject.SetActive(true);
            IsActive = true;
        }
        
        private void Deactivate()
        {
            gameObject.SetActive(false);
            IsActive = false;
        }
    }
}