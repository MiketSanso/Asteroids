using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "AsteroidData", order = 0)]
    public class AsteroidData : ScriptableObject
    {
        [field: SerializeField] public Vector2 Velocity { get; private set; }
        [field: SerializeField] public float SprayVelocity { get; private set; }
        [field: SerializeField] public int ScoreSize { get; private set; }
    }
}