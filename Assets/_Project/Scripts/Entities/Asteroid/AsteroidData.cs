using UnityEngine;

namespace GameScene.Entities.Asteroid
{
    [CreateAssetMenu(fileName = "Asteroid", menuName = "Asteroid", order = 0)]
    public class AsteroidData : ScriptableObject
    {
        [field: SerializeField] public AsteroidUI Prefab { get; private set; }
        [field: SerializeField] public Vector2 Speed { get; private set; }
        [field: SerializeField] public float SpraySpeed { get; private set; }
        [field: SerializeField] public Transform TransformParent { get; private set; }
    }
}