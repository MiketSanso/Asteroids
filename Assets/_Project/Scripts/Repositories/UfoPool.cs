using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;

namespace GameScene.Repositories
{
    public class UfoPool : PoolObjects
    {
        public Ufo[] Ufos { get; private set; }
    
        [Inject] private readonly PlayerUI _playerUI;
    
        private void CreateUfoPool(int poolSize,
            Ufo prefab,
            Transform transformParent)
        {
            Ufos = new Ufo[poolSize];
    
            for (int i = 0; i < poolSize; i++)
            {
                Transform transformSpawn = GetRandomTransform();
                Ufos[i] = prefab.Create(transformSpawn, transformParent, _playerUI);
                Ufos[i].Deactivate();
            }
        }
        
        public override void DeactivateObjects()
        {
            foreach (Ufo bullet in Ufos)
            {
                bullet.Deactivate();
            }
        }
    }
}