using GameScene.Entities.Player;
using Zenject;
using UnityEngine;
using GameScene.Repositories;
using GameScene.Factories;
using GameScene.Factories.ScriptableObjects;

namespace GameScene.Level
{
    public class EntryPoint : MonoInstaller
    {
        [SerializeField] private AsteroidFactoryData _asteroidFactoryData;
        [SerializeField] private BulletFactoryData _bulletFactoryData;
        [SerializeField] private UfoFactoryData _ufoFactoryData;
        
        private BulletFactory _bulletFactory;
        private AsteroidFactory _asteroidFactory;
        private UfoFactory _ufoFactory;
        private Shoot _shoot;
        private EndPanel _endPanel;
        private ScoreInfo _scoreInfo;
        
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
            _bulletFactory.Destroy();
            _ufoFactory.Destroy();
            _asteroidFactory.Destroy();
        }

        private void StartGame()
        {
            _bulletFactory = Container.Instantiate<BulletFactory>(new object[] { _bulletFactoryData });
            _ufoFactory = new UfoFactory(_ufoFactoryData);
            _asteroidFactory = new AsteroidFactory(_asteroidFactoryData);
            
            _scoreInfo.Initialize(_asteroidFactory, _ufoFactory);
            _shoot.Initialize(_bulletFactory);
            _endPanel.Initialize(_scoreInfo);
        }
    }
}