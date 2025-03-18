using UnityEngine;
using GameScene.Entities.Player;
using Zenject;

namespace GameScene.Repositories
{
    public abstract class PoolObjects
    {
        [Inject] private readonly PlayerUI _playerUI;
        private Transform[] _transformsSpawn;
        
        protected PoolObjects()
        {
            _playerUI.OnDeath += DeactivateObjects;
        }
        
        public void Destroy()
        {
            _playerUI.OnDeath -= DeactivateObjects;
        }
        
        public Transform GetRandomTransform()
        {
            int randomIndex = Random.Range(0, _transformsSpawn.Length);
            return _transformsSpawn[randomIndex];
        }

        public abstract void DeactivateObjects();
    }
}