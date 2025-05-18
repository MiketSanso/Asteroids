using System;
using Zenject;

namespace GameScene.Common
{
    public interface IInputService : ITickable
    {
        public event Action OnCanShotLaser;
        public event Action OnCanShotBullet;
        public event Action OnCanRotate;
        public event Action OnCanMove;
    }
}