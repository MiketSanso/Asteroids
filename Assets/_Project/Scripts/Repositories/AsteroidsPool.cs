using UnityEngine;
using GameScene.Entities.Asteroid;

namespace GameScene.Repositories
{
    public class AsteroidsPool : PoolObjects
    {
        public AsteroidUI[] Asteroids { get; private set; }
        public AsteroidUI[] SmallAsteroids { get; private set; }
        
        private AsteroidsPool(AsteroidData smallAsteroidData,
            AsteroidData asteroidData,
            int sizeAsteroidPool,
            Transform transformParent)
        {
            Asteroids = new AsteroidUI[sizeAsteroidPool];
            for (int i = 0; i < sizeAsteroidPool; i ++)
            {
                Asteroids[i] = asteroidData.Prefab.Create(GetRandomTransform(), transformParent, asteroidData.Speed, asteroidData.SpraySpeed);
            }
            
            SmallAsteroids = new AsteroidUI[sizeAsteroidPool * 2];
            for (int i = 0; i < sizeAsteroidPool * 2; i++)
            {
                SmallAsteroids[i] = smallAsteroidData.Prefab.Create(GetRandomTransform(), transformParent, smallAsteroidData.Speed, smallAsteroidData.SpraySpeed);
            }
        }

        protected override void DeactivateObjects()
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