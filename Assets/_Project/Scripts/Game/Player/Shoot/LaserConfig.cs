using System;

namespace GameScene.Repositories.Configs
{
    [Serializable]
    public class LaserConfig : Config
    {
        public float LaserRange;
        public float TimeVisibleLaser;
        public float FixedTimeRechargeLaser;
        public float StepTimeRecharge;
        public float StepTimeDamage;
        public int MaxCountLaserShoots;
    }
}