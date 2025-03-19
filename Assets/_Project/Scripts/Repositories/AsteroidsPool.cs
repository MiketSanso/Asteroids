using UnityEngine;
using GameScene.Entities.Asteroid;

namespace GameScene.Repositories
{
    public class AsteroidsPool : PoolObjects
    {
        public AsteroidUI[] Asteroids { get; private set; }
        public AsteroidUI[] SmallAsteroids { get; private set; }
        
        public AsteroidsPool(AsteroidData smallAsteroidData,
            AsteroidData asteroidData,
            int sizeAsteroidPool,
            Transform transformParent)
        {
            Asteroids = new AsteroidUI[sizeAsteroidPool];
            for (int i = 0; i < sizeAsteroidPool; i ++)
            {
                Asteroids[i] = Create(asteroidData.Prefab, 
                    GetRandomTransform(), 
                    transformParent, 
                    asteroidData.Speed, 
                    asteroidData.SpraySpeed);
            }
            
            SmallAsteroids = new AsteroidUI[sizeAsteroidPool * 2];
            for (int i = 0; i < sizeAsteroidPool * 2; i++)
            {
                SmallAsteroids[i] = Create(smallAsteroidData.Prefab, 
                    GetRandomTransform(),
                    transformParent, 
                    smallAsteroidData.Speed, 
                    smallAsteroidData.SpraySpeed);
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
        
        private AsteroidUI Create(AsteroidUI prefab,
            Vector2 positionSpawn, 
            Transform transformParent, 
            Vector2 velocity, 
            float sprayVelocity)
        {
            AsteroidUI asteroidUI = Object.Instantiate(prefab, positionSpawn, Quaternion.identity, transformParent);
            Asteroid asteroid = new Asteroid(velocity, sprayVelocity);
            asteroidUI.Initialize(asteroid);
            asteroidUI.Deactivate();
            
            return asteroidUI;
        }
    }
}