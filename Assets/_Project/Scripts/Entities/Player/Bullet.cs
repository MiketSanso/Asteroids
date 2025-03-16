using GameScene.Entities.Asteroid;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class Bullet : MonoBehaviour, IEnemyDestroyer
    {
        public Vector3 SpawnPosition { get; private set; }

        public Bullet Create(Vector3 spawnPosition)
        {
            Bullet bullet = Instantiate(this, spawnPosition, Quaternion.identity);
            SpawnPosition = spawnPosition;
            return bullet;
        }
        
        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}