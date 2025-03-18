using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Repositories
{
    public class BulletPool : PoolObjects
    {
        public Bullet[] Bullets { get; private set; }
        
        [Inject] private readonly PlayerUI _playerUI;
        
        private void CreateBulletsPool(int poolSize, 
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

        public override void DeactivateObjects()
        {
            foreach (Bullet bullet in Bullets)
            {
                bullet.Deactivate();
            }
        }
    }
}