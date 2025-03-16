using UnityEngine;
using GameScene.Entities.Asteroid;
using System;
using GameScene.Entities.Player;
using GameScene.Entities.UFO;

namespace _Project.Scripts.Entities
{
    public class PoolObjects
    {
        public event Action OnAsteroidDestroyed;

        public AsteroidUI[] Asteroids { get; private set; }
        public AsteroidUI[] SmallAsteroids { get; private set; }
        public UFO[] Ufos { get; private set; }
        public Bullet[] Bullets { get; private set; }
        
        public PoolObjects(AsteroidData smallAsteroidData,
            AsteroidData asteroidData,
            UFO prefabUfo,
            Bullet prefabBullet,
            int sizeAsteroidPool, 
            int sizeUFOPool,
            int sizeBulletPool,
            Transform[] transformsSpawn,
            Player player)
        {
            CreateAsteroidPools(smallAsteroidData, 
                asteroidData, 
                sizeAsteroidPool, 
                transformsSpawn);
            
            CreateUFOsPool(sizeUFOPool, 
                prefabUfo, 
                transformsSpawn, 
                player);

            CreateBulletsPool(sizeBulletPool, prefabBullet, player);
        }

        private void CreateAsteroidPools(AsteroidData smallAsteroidData,
            AsteroidData asteroidData,
            int sizeAsteroidPoolPool,
            Transform[] transformsSpawn)
        {
            Asteroids = new AsteroidUI[sizeAsteroidPoolPool];
            for (int i = 0; i < sizeAsteroidPoolPool; i ++)
            {
                Transform transformSpawn = GetRandomTransform(transformsSpawn);
                Asteroids[i] = asteroidData.Prefab.Create(transformSpawn, asteroidData.TransformParent, asteroidData.Speed, asteroidData.SpraySpeed);
                Asteroids[i].Asteroid.OnDestroyed += AsteroidDestroyed;
                Asteroids[i].Asteroid.OnDestroyed1 += ActivateSmallAsteroids;
            }
            
            SmallAsteroids = new AsteroidUI[sizeAsteroidPoolPool * 2];
            for (int i = 0; i < sizeAsteroidPoolPool * 2; i++)
            {
                Transform transformSpawn = GetRandomTransform(transformsSpawn);
                SmallAsteroids[i] = smallAsteroidData.Prefab.Create(transformSpawn, smallAsteroidData.TransformParent, smallAsteroidData.Speed, smallAsteroidData.SpraySpeed);
                SmallAsteroids[i].Asteroid.OnDestroyed += AsteroidDestroyed;
            }
        }

        private void CreateUFOsPool(int poolSize, UFO prefab, Transform[] transformsSpawn, Player player)
        {
            Ufos = new UFO[poolSize];
            
            for (int i = 0; i < poolSize; i++)
            {
                Transform transformSpawn = GetRandomTransform(transformsSpawn);
                Ufos[i] = prefab.Create(transformSpawn, player);
                Ufos[i].Deactivate();
            }
        }
        
        private void CreateBulletsPool(int poolSize, Bullet prefab, Player player)
        {
            Bullets = new Bullet[Ufos.Length];
            for (int i = 0; i < poolSize; i++)
            {
                Bullets[i] = prefab.Create(player.transform.position);
            }
        }

        private Transform GetRandomTransform(Transform[] transformsSpawn)
        {
            int randomIndex = UnityEngine.Random.Range(0, transformsSpawn.Length);
            return transformsSpawn[randomIndex];
        }
        
        public void Destroy()
        {
            for (int i = 0; i < SmallAsteroids.Length; i ++)
            {
                if (i < Asteroids.Length)
                {
                    Asteroids[i].Asteroid.OnDestroyed -= AsteroidDestroyed;
                    Asteroids[i].Asteroid.OnDestroyed1 -= ActivateSmallAsteroids;
                }
                
                SmallAsteroids[i].Asteroid.OnDestroyed -= AsteroidDestroyed;
            }
        }
        
        private void ActivateSmallAsteroids(Transform transform)
        {
            int countActivatedAsteroids = 0;
            
            foreach (AsteroidUI asteroid in SmallAsteroids)
            {
                if (countActivatedAsteroids < 2 && !asteroid.gameObject.activeSelf)
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