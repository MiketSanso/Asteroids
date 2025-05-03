using GameScene.Entities.Player;
using UnityEngine;

namespace GameScene.Interfaces
{
    public interface IInputService
    {
        public void Shot(Transform transformSpawn);

        public void Move(PlayerMovement playerMovement);
    }
}