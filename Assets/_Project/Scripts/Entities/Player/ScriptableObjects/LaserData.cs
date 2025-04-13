using UnityEngine;

namespace GameScene.Entities.Player
{
    [CreateAssetMenu(fileName = "LaserData", menuName = "LaserData", order = 0)]
    public class LaserData : ScriptableObject
    {
        [field: SerializeField] public float LaserRange { get; private set; }
        [field: SerializeField] public float TimeVisibleLaser { get; private set; }
        [field: SerializeField] public float FixedTimeRechargeLaser { get; private set; }
        [field: SerializeField] [Min(0)] public float StepTimeRecharge { get; private set; }
        [field: SerializeField] [Min(0)] public float StepTimeDamage { get; private set; }
        [field: SerializeField] public int MaxCountLaserShoots { get; private set; }
    }
}