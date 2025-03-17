using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;
using UnityEngine;
using GameScene.Repositories;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AsteroidFactory asteroidFactory;
        [SerializeField] private UFOFactory _ufoFactory;
        [SerializeField] private Shoot _shoot;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private AsteroidData _smallAsteroidData;
        [SerializeField] private Ufo _prefabUFO;
        [SerializeField] private Bullet _prefabBullet;
        [SerializeField] private int _sizeAsteroidsPool;
        [SerializeField] private int _sizeUfoPool;
        [SerializeField] private int _sizeBulletsPool;
        [SerializeField] private Transform[] _transformsSpawn;
        [SerializeField] private Transform transformParent;
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private ScoreInfo _scoreInfo;
        
        private PoolObjects _poolObjects;

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
            _poolObjects = new PoolObjects(_smallAsteroidData, 
                _asteroidData, 
                _prefabUFO,
                _prefabBullet, 
                _sizeAsteroidsPool, 
                _sizeUfoPool, 
                _sizeBulletsPool,
                _transformsSpawn,
                transformParent,
                _playerUI);
            
            _scoreInfo.Initialize(_endPanel, _poolObjects);
            _shoot.Initialize(_poolObjects);
            _ufoFactory.Initialize(_playerUI, _poolObjects, _endPanel);
            asteroidFactory.Initialize(_poolObjects, _endPanel);
            _endPanel.Initialize(_playerUI, _scoreInfo);
            _gameUI.Initialize(_playerUI, _shoot); 
        }
    }
}