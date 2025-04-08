using UnityEngine;
using GameScene.Entities.Asteroid;

namespace GameScene.Factories.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AsteroidFactoryData", menuName = "AsteroidFactoryData", order = 0)]
    public class AsteroidFactoryData : FactoryData
    {
        [field: SerializeField] public AsteroidUI SmallPrefab { get; private set; }
        [field: SerializeField] public AsteroidUI Prefab { get; private set; }
        [field: SerializeField] public int countFragments;
    }
}