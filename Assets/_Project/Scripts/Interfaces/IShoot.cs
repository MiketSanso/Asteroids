using UnityEngine;

namespace GameScene.Interfaces
{
    public interface IShoot
    {
        public void Shot(Transform transformSpawn);
        
        public void CheckLaserShot();
    }
}