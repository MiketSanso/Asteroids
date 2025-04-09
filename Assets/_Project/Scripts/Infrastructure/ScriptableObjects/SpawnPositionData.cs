using UnityEngine;

namespace GameScene.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnPositionData", menuName = "SpawnPositionData", order = 0)]
    public class SpawnPositionData : ScriptableObject
    {
        [field: SerializeField] public float MinPositionX { get; private set; }
        [field: SerializeField] public float MaxPositionX { get; private set; }
        [field: SerializeField] public float MinPositionY { get; private set; }
        [field: SerializeField] public float MaxPositionY { get; private set; }
    }
}