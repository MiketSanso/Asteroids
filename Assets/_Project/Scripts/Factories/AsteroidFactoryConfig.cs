using System;

namespace GameScene.Models.Configs
{
    [Serializable]
    public class AsteroidFactoryConfig : Config
    {
        public int SizePool;
        public int CountFragments;
    }
}