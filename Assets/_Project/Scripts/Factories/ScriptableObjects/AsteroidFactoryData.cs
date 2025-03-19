using UnityEngine;
using GameScene.Entities.Asteroid;

namespace GameScene.Factories.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AsteroidFactoryData", menuName = "AsteroidFactoryData", order = 0)]
    public class AsteroidFactoryData : FactoryData
    {
        [field: SerializeField] public AsteroidData SmallAsteroidData { get; private set; }
        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
    }
}