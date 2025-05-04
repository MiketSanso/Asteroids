using System;
using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.Player
{
    public class KeyboardInput : IInputService
    {
        public event Action OnCanShotLaser;
        public event Action OnCanShotBullet;
        public event Action OnCanRotate;
        public event Action OnCanMove;
        
        public void Shot()
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
        
        public void Move()
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