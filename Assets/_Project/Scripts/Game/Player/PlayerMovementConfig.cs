using System;

namespace GameScene.Models.Configs
{
    [Serializable]
    public class PlayerMovementConfig : Config
    {
        public float SpeedRotation;
        public float SpeedMove;
        public float MaxSpeed;
    }
}