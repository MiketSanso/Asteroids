using System;

namespace GameScene.Models.Configs
{
    [Serializable]
    public class UfoFactoryConfig : Config
    {
        public int SizePool;
        public float MinTimeSpawn;
        public float MaxTimeSpawn;
    }
}