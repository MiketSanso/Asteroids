using System;

namespace GameScene.Configs
{
    [Serializable]
    public class PlayerMovementConfig : Config
    {
        public float SpeedRotation;
        public float SpeedMove;
        public float MaxSpeed;
    }
}