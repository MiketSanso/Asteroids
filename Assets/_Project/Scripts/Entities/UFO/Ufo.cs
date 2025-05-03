using _Project.Scripts.Infrastructure;
using GameScene.Interfaces;
using UnityEngine;
using GameScene.Configs;

namespace GameScene.Entities.UFOs
{
    public class Ufo : IPooledObject
    {
        public delegate void DestroyedEventHandler(int scoreSize, Transform transform);
        public event DestroyedEventHandler OnDestroy;
        
        private readonly GameObject _gameObject;
        private readonly UfoConfig _ufoConfig;

        public Ufo(UfoConfig ufoConfig, GameObject gameObject)
        {
            _ufoConfig = ufoConfig;
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
            OnDestroy?.Invoke(_ufoConfig.ScoreSize, _gameObject.transform);
        }
    }
}
