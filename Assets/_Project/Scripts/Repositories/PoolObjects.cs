using UnityEngine;
using GameScene.Entities.Player;
using Zenject;
using GameScene.Level;

namespace GameScene.Repositories
{
    public abstract class PoolObjects
    {
        private PlayerUI _playerUI;
        private SpawnTransform _spawnTransform;
        
        protected PoolObjects()
        {
            _playerUI.OnDeath += DeactivateObjects;
        }
        
        [Inject]
        public void Construct(PlayerUI playerUI, SpawnTransform spawnTransform)
        {
            _playerUI = playerUI;
            _spawnTransform = spawnTransform;
        }
        
        public void Destroy()
        {
            _playerUI.OnDeath -= DeactivateObjects;
        }
        
        public Vector2 GetRandomTransform()
        {
            return _spawnTransform.GetPosition();
        }

        protected abstract void DeactivateObjects();
    }
}