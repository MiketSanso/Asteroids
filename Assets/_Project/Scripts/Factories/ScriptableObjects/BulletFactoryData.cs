using UnityEngine;
using GameScene.Entities.Player;

namespace GameScene.Factories.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BulletFactoryData", menuName = "BulletFactoryData", order = 0)]
    public class BulletFactoryData : FactoryData
    {
        [field: SerializeField] public Bullet Prefab { get; private set; }
    }
}