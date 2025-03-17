using UnityEngine;

namespace GameScene.Entities.Teleport
{
    public class Teleporter : MonoBehaviour
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
    }
}
