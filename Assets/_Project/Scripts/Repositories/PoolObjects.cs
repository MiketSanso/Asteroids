using UnityEngine;
using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;

namespace GameScene.Repositories
{
    public class PoolObjects
    {
        public AsteroidUI[] Asteroids { get; private set; }
        public AsteroidUI[] SmallAsteroids { get; private set; }
        public Ufo[] Ufos { get; private set; }
        public Bullet[] Bullets { get; private set; }
        
        private readonly PlayerUI _player;
        private Transform[] _transformsSpawn;
        
        public PoolObjects(AsteroidData smallAsteroidData,
            AsteroidData asteroidData,
            Ufo prefabUFo,
            Bullet prefabBullet,
            int sizeAsteroidPool, 
            int sizeUFOPool,
            int sizeBulletPool,
            Transform[] transformsSpawn,
            Transform transformParent,
            PlayerUI playerUI)
        {
            _transformsSpawn = transformsSpawn;
            
            CreateAsteroidPools(smallAsteroidData, 
                asteroidData, 
                sizeAsteroidPool,
                transformParent);
            
            CreateUFOsPool(sizeUFOPool, 
                prefabUFo,
                transformParent,
                playerUI);

            CreateBulletsPool(sizeBulletPool, 
                prefabBullet, 
                playerUI, 
                transformParent);
            
            _player = playerUI;
            playerUI.OnDeath += DeactivateObjects;
        }
        
        public void Destroy()
        {
            _player.OnDeath -= DeactivateObjects;
        }
        
        public Transform GetRandomTransform()
        {
            int randomIndex = UnityEngine.Random.Range(0, _transformsSpawn.Length);
            return _transformsSpawn[randomIndex];
        }

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

        private void CreateUFOsPool(int poolSize, 
            Ufo prefab,
            Transform transformParent,
            PlayerUI playerUI)
        {
            Ufos = new Ufo[poolSize];
            
            for (int i = 0; i < poolSize; i++)
            {
                Transform transformSpawn = GetRandomTransform();
                Ufos[i] = prefab.Create(transformSpawn, transformParent, playerUI);
                Ufos[i].Deactivate();
            }
        }
        
        private void CreateBulletsPool(int poolSize, Bullet prefab, PlayerUI playerUI, Transform transformParent)
        {
            Bullets = new Bullet[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                Bullets[i] = prefab.Create(playerUI, transformParent);
                Bullets[i].Deactivate();
            }
        }

        private void DeactivateObjects()
        {
            foreach (AsteroidUI asteroid in Asteroids)
            {
                asteroid.Deactivate();
            }
            
            foreach (AsteroidUI asteroid in SmallAsteroids)
            {
                asteroid.Deactivate();
            }
            
            foreach (Ufo ufo in Ufos)
            {
                ufo.Deactivate();
            }
        }
    }
}