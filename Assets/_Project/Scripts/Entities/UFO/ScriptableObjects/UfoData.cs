using UnityEngine;

namespace GameScene.Entities.UFOs
{
    [CreateAssetMenu(fileName = "UfoData", menuName = "UfoData", order = 0)]
    public class UfoData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int ScoreSize { get; private set; }
    }
}