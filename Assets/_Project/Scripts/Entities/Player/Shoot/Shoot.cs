using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Entities.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform _transformSpawn;
        
        private IShoot _shoot;

        [Inject]
        private void Construct(IShoot shoot)
        {
            _shoot = shoot;
        }

        private void Update()
        {
            _shoot.Shot(_transformSpawn);
            _shoot.CheckLaserShot();
        }
    }
}