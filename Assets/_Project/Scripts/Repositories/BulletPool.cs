using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Repositories
{
    public class BulletPool : PoolObjects
    {
        public Bullet[] Bullets { get; private set; }
        
        private PlayerUI _playerUI;
        
        private BulletPool(int poolSize, 
            Bullet prefab, 
            Transform transformParent)
        {
            Bullets = new Bullet[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                Bullets[i] = prefab.Create(_playerUI, transformParent);
                Bullets[i].Deactivate();
            }
        }
        
        [Inject]
        public void Construct(PlayerUI playerUI)
        {
            _playerUI = playerUI;
        }

        protected override void DeactivateObjects()
        {
            foreach (Bullet bullet in Bullets)
            {
                bullet.Deactivate();
            }
        }
    }
}