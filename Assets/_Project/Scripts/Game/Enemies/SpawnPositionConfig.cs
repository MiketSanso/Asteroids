using System;

namespace GameScene.Repositories.Configs
{
    [Serializable]
    public class SpawnPositionConfig : Config
    {
        public float MinPositionX;
        public float MaxPositionX;
        public float MinPositionY;
        public float MaxPositionY;
    }
}