using System;

namespace GameScene.Configs
{
    [Serializable]
    public class UfoFactoryConfig : Config
    {
        public int SizePool;
        public float MinTimeSpawn;
        public float MaxTimeSpawn;
    }
}