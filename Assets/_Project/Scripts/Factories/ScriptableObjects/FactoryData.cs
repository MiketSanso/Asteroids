using UnityEngine;
using GameScene.Level;
using Zenject;

namespace GameScene.Factories.ScriptableObjects
{
    public abstract class FactoryData : ScriptableObject
    {
        [field: SerializeField] public int SizePool { get; private set; }
    }
}