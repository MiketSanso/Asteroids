using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Repositories
{
    public class BulletPool : PoolObjects
    {
        public Bullet[] Bullets { get; private set; }
        
        private PlayerUI _playerUI;
        
        public BulletPool(Bullet prefab, 
            int poolSize, 
            Transform transformParent)
        {
            Bullets = new Bullet[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                Bullets[i] = Create(prefab, _playerUI, transformParent);
                Bullets[i].Deactivate();
            }
        }
        
        [Inject]
        public void Construct(PlayerUI playerUI)
        {
            _playerUI = playerUI;
        }

        public override void DeactivateObjects()
        {
            foreach (Bullet bullet in Bullets)
            {
                bullet.Deactivate();
            }
        }
        
        private Bullet Create(Bullet prefab, PlayerUI player, Transform transformParent)
        {
            Bullet bullet = Object.Instantiate(prefab, player.transform.position, Quaternion.identity, transformParent);
            return bullet;
        }
    }
}