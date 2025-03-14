using System;
using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    public class AsteroidsPool
    {
        public event Action OnAsteroidDestroyed;
        
        public readonly Asteroid[] Asteroids;
        public readonly Asteroid[] SmallAsteroids;

        public AsteroidsPool(Asteroid prefab, 
            int sizePool, 
            Transform[] transformsSpawn, 
            Transform transformParent, 
            Asteroid smallPrefab,
            Vector2 speed,
            Vector2 speedSmall)
        {
            Asteroids = new Asteroid[sizePool];
            for (int i = 0; i < sizePool; i ++)
            {
                int randomIndex = UnityEngine.Random.Range(0, transformsSpawn.Length);
                Transform transformSpawn = transformsSpawn[randomIndex];
                Asteroids[i] = prefab.Create(transformSpawn, transformParent, speed);
                Asteroids[i].OnDestroyed += AsteroidDestroyed;
                Asteroids[i].OnDestroyed1 += ActivateSmallAsteroids;
            }
            
            SmallAsteroids = new Asteroid[sizePool * 2];
            for (int i = 0; i < sizePool * 2; i++)
            {
                int indexAsteroid = i < sizePool ? i : i - sizePool;
                SmallAsteroids[i] = smallPrefab.Create(Asteroids[indexAsteroid].transform, transformParent, speedSmall);
                SmallAsteroids[i].OnDestroyed += AsteroidDestroyed;
            }
        }
        
        public void Destroy()
        {
            for (int i = 0; i < SmallAsteroids.Length; i ++)
            {
                if (i < Asteroids.Length)
                {
                    Asteroids[i].OnDestroyed -= AsteroidDestroyed;
                    Asteroids[i].OnDestroyed1 -= ActivateSmallAsteroids;
                }
                
                SmallAsteroids[i].OnDestroyed -= AsteroidDestroyed;
            }
        }
        
        private void ActivateSmallAsteroids(Transform transform)
        {
            int countActivatedAsteroids = 0;
            
            foreach (Asteroid asteroid in SmallAsteroids)
            {
                if (countActivatedAsteroids < 2 && asteroid.gameObject.activeSelf)
                {
                    countActivatedAsteroids++;
                    asteroid.Activate(transform);
                }
                else if (countActivatedAsteroids == 2)
                {
                    break;
                }
            }
        }

        private void AsteroidDestroyed()
        {
            OnAsteroidDestroyed?.Invoke();
        }
    }
}