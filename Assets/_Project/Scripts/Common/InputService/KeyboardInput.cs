using System;
using UnityEngine;

namespace GameScene.Common
{
    public class KeyboardInput : IInputService
    {
        public event Action OnCanShotLaser;
        public event Action OnCanShotBullet;
        public event Action OnCanRotate;
        public event Action OnCanMove;
        
        public void Tick()
        {
            Shot();
            Move();
        }
        
        private void Shot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnCanShotBullet?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnCanShotLaser?.Invoke();
            }
        }
        
        private void Move()
        {
            if (Input.GetButton("Horizontal"))
            {
                OnCanRotate?.Invoke();
            }

            if (Input.GetKey(KeyCode.W))
            {
                OnCanMove?.Invoke();
            }
        }
    }
}