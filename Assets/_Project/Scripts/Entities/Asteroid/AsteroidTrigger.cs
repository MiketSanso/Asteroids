using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidTrigger : MonoBehaviour, IDestroyableEnemy
    {
        private Asteroid _asteroid;

        public void Destroy()
        {
            _asteroid.Destroy(gameObject);
        }

        public void Initialize(Asteroid asteroid)
        {
            _asteroid = asteroid;
        }
    }
}