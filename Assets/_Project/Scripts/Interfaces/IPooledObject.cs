using UnityEngine;

namespace GameScene.Interfaces
{
    public interface IPooledObject
    {
        public void Activate(Vector2 position);
        public void Deactivate();
    }
}