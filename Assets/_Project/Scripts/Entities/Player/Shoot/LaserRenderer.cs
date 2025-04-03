using UnityEngine;

namespace GameScene.Entities.Player
{
    public class LaserRenderer : MonoBehaviour
    {
        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }

    }
}