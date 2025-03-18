using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;
using UnityEngine;
using GameScene.Repositories;
using Zenject;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AsteroidFactory asteroidFactory;
        [SerializeField] private UfoFactory _ufoFactory;
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private AsteroidData _smallAsteroidData;
        [SerializeField] private Ufo _prefabUFO;
        [SerializeField] private Bullet _prefabBullet;
        [SerializeField] private int _sizeAsteroidsPool;
        [SerializeField] private int _sizeUfoPool;
        [SerializeField] private int _sizeBulletsPool;
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private Transform _transformParent;
        [SerializeField] private ScoreInfo _scoreInfo;
        
        [Inject] private EndPanel _endPanel;
        [Inject] private PlayerUI _playerUI;
        [Inject] private Shoot _shoot;

        private void Start()
        {
            StartGame();
        }

        private void OnDestroy()
        {
            _poolObjects.Destroy();
        }

        private void StartGame()
        {
            _scoreInfo.Initialize(_poolObjects);
            _shoot.Initialize(_poolObjects);
            _ufoFactory.Initialize(_poolObjects);
            asteroidFactory.Initialize(_poolObjects);
            _endPanel.Initialize(_scoreInfo);
        }
    }
}