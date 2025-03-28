using UnityEngine;

namespace GameScene.Factories.ScriptableObjects
{
    public abstract class FactoryData : ScriptableObject
    {
        [field: SerializeField] public int SizePool { get; private set; }
    }
}