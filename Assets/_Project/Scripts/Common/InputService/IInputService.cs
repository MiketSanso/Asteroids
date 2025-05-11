using System;

namespace GameScene.Common
{
    public interface IInputService
    {
        public event Action OnCanShotLaser;
        public event Action OnCanShotBullet;
        public event Action OnCanRotate;
        public event Action OnCanMove;
        
        public void Shot();

        public void Move();
    }
}