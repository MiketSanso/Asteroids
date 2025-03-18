using UnityEngine;
using Zenject;
using GameScene.Entities.Asteroid;

namespace GameScene.Repositories
{
    public class AsteroidsPool : PoolObjects
    {
        public AsteroidUI[] Asteroids { get; private set; }
        public AsteroidUI[] SmallAsteroids { get; private set; }
        
        private Transform[] _transformsSpawn;
        
        private void CreateAsteroidPools(AsteroidData smallAsteroidData,
            AsteroidData asteroidData,
            int sizeAsteroidPoolPool,
            Transform transformParent)
        {
            Asteroids = new AsteroidUI[sizeAsteroidPoolPool];
            for (int i = 0; i < sizeAsteroidPoolPool; i ++)
            {
                Transform transformSpawn = GetRandomTransform();
                Asteroids[i] = asteroidData.Prefab.Create(transformSpawn, transformParent, asteroidData.Speed, asteroidData.SpraySpeed);
            }
            
            SmallAsteroids = new AsteroidUI[sizeAsteroidPoolPool * 2];
            for (int i = 0; i < sizeAsteroidPoolPool * 2; i++)
            {
                Transform transformSpawn = GetRandomTransform();
                SmallAsteroids[i] = smallAsteroidData.Prefab.Create(transformSpawn, transformParent, smallAsteroidData.Speed, smallAsteroidData.SpraySpeed);
            }
        }

        public override void DeactivateObjects()
        {
            foreach (AsteroidUI bullet in Asteroids)
            {
                bullet.Deactivate();
            }
            
            foreach (AsteroidUI bullet in SmallAsteroids)
            {
                bullet.Deactivate();
            }
        }
    }
}