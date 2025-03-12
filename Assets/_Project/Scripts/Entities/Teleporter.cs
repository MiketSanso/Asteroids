using UnityEngine;

namespace GameScene.Level
{
    public class Teleporter : MonoBehaviour
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
    }
}
