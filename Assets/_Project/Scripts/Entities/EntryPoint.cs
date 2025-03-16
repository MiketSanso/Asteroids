using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFO;
using UnityEngine;
using _Project.Scripts.Entities;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AsteroidFabric _asteroidFabric;
        [SerializeField] private UFOFactory _ufoFactory;
        [SerializeField] private Shoot _shoot;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private Player _player;
        
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private AsteroidData _smallAsteroidData;
        [SerializeField] private UFO _prefabUFO;
        [SerializeField] private Bullet _prefabSmallUFO;
        
        private PoolObjects _poolObjects;

        public void Start()
        {
            _poolObjects = new PoolObjects();
            
            _shoot.Initialize(_poolObjects);
            _ufoFactory.Initialize(_player, _poolObjects);
            _asteroidFabric.Initialize(_poolObjects);
            _endPanel.Initialize(_player);
            _gameUI.Initialize(_player);
        }
    }
}