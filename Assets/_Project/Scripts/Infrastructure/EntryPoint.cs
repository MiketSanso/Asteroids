using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;
using UnityEngine;
using GameScene.Repositories;
using Zenject;
using GameScene.Factories;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private AsteroidData _smallAsteroidData;
        [SerializeField] private Ufo _prefabUFO;
        [SerializeField] private Bullet _prefabBullet;
        [SerializeField] private int _sizeAsteroidsPool;
        [SerializeField] private int _sizeUfoPool;
        [SerializeField] private int _sizeBulletsPool;
        [SerializeField] private Transform _transformParent;
        
        private ScoreInfo _scoreInfo;
        private AsteroidFactory asteroidFactory;
        private UfoFactory _ufoFactory;
        private Shoot _shoot;
        private EndPanel _endPanel;
        private AsteroidsPool _asteroidsPool;
        private UfoPool _ufoPool;
        private BulletPool _bulletPool;

        private void Start()
        {
            StartGame();
        }

        [Inject]
        public void Construct(Shoot shoot, EndPanel endPanel)
        {
            _shoot = shoot;
            _endPanel = endPanel;
        }

        private void OnDestroy()
        {
            _bulletPool.Destroy();
            _ufoPool.Destroy();
            _asteroidsPool.Destroy();
        }

        private void StartGame()
        {
            _bulletPool = new BulletPool();
            _ufoPool = new UfoPool();
            _asteroidsPool = new AsteroidsPool();
            
            _scoreInfo.Initialize(_asteroidsPool, _ufoPool);
            _shoot.Initialize(_bulletPool);
            _ufoFactory.Initialize(_ufoPool);
            asteroidFactory.Initialize(_asteroidsPool);
            _endPanel.Initialize(_scoreInfo);
        }
    }
}