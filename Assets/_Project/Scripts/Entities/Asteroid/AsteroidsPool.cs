using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidsPool
    {
        public readonly Asteroid[] Asteroids;

        public AsteroidsPool(Asteroid prefab, int sizePool, Transform transformSpawn, Transform transformParent)
        {
            Asteroids = new Asteroid[sizePool];

            for (int i = 0; i < sizePool; i++)
            {
                Asteroids[i] = prefab.Create(transformSpawn, transformParent);
            }
        }
    }
}