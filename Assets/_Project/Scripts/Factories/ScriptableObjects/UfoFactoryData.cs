using UnityEngine;
using GameScene.Entities.UFOs;

namespace GameScene.Factories.ScriptableObjects
{
    [CreateAssetMenu(fileName = "UfoFactoryData", menuName = "UfoFactoryData", order = 0)]
    public class UfoFactoryData : FactoryData
    {
        [field: SerializeField] public UfoMovement Prefab { get; private set; }        
        [field: SerializeField] public float MinTimeSpawn { get; private set; }
        [field: SerializeField] public float MaxTimeSpawn { get; private set; }
    }
}