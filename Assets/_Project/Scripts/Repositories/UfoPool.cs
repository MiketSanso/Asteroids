using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;

namespace GameScene.Repositories
{
    public class UfoPool : PoolObjects
    {
        public Ufo[] Ufos { get; private set; }
    
        private PlayerUI _playerUI;
    
        private UfoPool(int poolSize,
            Ufo prefab,
            Transform transformParent)
        {
            Ufos = new Ufo[poolSize];
    
            for (int i = 0; i < poolSize; i++)
            {
                Ufos[i] = prefab.Create(GetRandomTransform(), transformParent);
                Ufos[i].Deactivate();
            }
        }
        
        [Inject]
        public void Construct(PlayerUI playerUI)
        {
            _playerUI = playerUI;
        }
        
        protected override void DeactivateObjects()
        {
            foreach (Ufo bullet in Ufos)
            {
                bullet.Deactivate();
            }
        }
    }
}