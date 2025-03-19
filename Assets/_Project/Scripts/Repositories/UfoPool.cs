using UnityEngine;
using GameScene.Entities.UFOs;

namespace GameScene.Repositories
{
    public class UfoPool : PoolObjects
    {
        public Ufo[] Ufos { get; private set; }
        
        public UfoPool(Ufo prefab,
            int poolSize,
            Transform transformParent)
        {
            Ufos = new Ufo[poolSize];
    
            for (int i = 0; i < poolSize; i++)
            {
                Ufos[i] = Create(prefab, GetRandomTransform(), transformParent);
                Ufos[i].Deactivate();
            }
        }
        
        protected override void DeactivateObjects()
        {
            foreach (Ufo bullet in Ufos)
            {
                bullet.Deactivate();
            }
        }
        
        private Ufo Create(Ufo prefab, Vector2 positionSpawn, Transform transformParent)
        {
            Ufo ufo = Object.Instantiate(prefab, positionSpawn, Quaternion.identity, transformParent);
            return ufo;
        }
    }
}