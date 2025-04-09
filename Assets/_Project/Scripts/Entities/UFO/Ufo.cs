using GameScene.Interfaces;
using UnityEngine;

namespace GameScene.Entities.UFOs
{
    public class Ufo : IPooledObject
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroyed;
        
        private readonly GameObject _gameObject;
        private readonly UfoData _ufoData;

        public Ufo(UfoData ufoData, GameObject gameObject)
        {
            _ufoData = ufoData;
            _gameObject = gameObject;
        }

        public void Activate(Vector2 positionSpawn)
        {
            _gameObject.transform.position = positionSpawn;
            _gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _gameObject.SetActive(false);
        }
        
        public void Destroy()
        {
            Deactivate();
            OnDestroyed?.Invoke(_ufoData.ScoreSize, _gameObject.transform);
        }
    }
}
