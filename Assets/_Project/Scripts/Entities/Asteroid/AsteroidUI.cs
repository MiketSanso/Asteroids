using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidUI : MonoBehaviour, IDestroyableEnemy
    {
        private Asteroid _asteroid;

        public void Initialize(Asteroid asteroid)
        {
            _asteroid = asteroid;
        }
        
        public void Destroy()
        {
            _asteroid.Destroy(gameObject);
        }
    }
}