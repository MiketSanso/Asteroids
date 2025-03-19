using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;
using UnityEngine;
using GameScene.Repositories;
using Zenject;
using GameScene.Factories;
using GameScene.Factories.ScriptableObjects;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
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

        }

        private void StartGame()
        {
            _bulletFactory = new BulletFactory(_bulletFactoryData);
            _ufoFactory = new UfoFactory(_ufoFactoryData);
            _asteroidFactory = new AsteroidFactory(_asteroidFactoryData);
            
            _scoreInfo.Initialize(_asteroidsPool, _ufoPool);
            _shoot.Initialize(_bulletPool);
            _endPanel.Initialize(_scoreInfo);
        }
    }
}