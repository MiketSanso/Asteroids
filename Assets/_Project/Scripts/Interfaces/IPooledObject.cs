using UnityEngine;

namespace GameScene.Interfaces
{
    public interface IPooledObject
    {
        public void Activate(Transform transform);
        public void Deactivate();
    }
}