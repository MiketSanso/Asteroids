using System;

namespace GameScene.Repositories.Configs
{
    [Serializable]
    public class AsteroidFactoryConfig : Config
    {
        public int SizePool;
        public int CountFragments;
    }
}