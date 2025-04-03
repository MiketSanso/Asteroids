using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;
using GameScene.Repositories;
using GameScene.Factories;
using GameScene.Factories.ScriptableObjects;

namespace GameScene.Level
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private LaserRenderer _laserRenderer;
        [SerializeField] private PlayerUI _player;
        [SerializeField] private SpawnTransform _spawnTransform;
        [SerializeField] private TransformParent _transformParent;
        [SerializeField] private AsteroidFactoryData _asteroidFactoryData;
        [SerializeField] private BulletFactoryData _bulletFactoryData;
        [SerializeField] private UfoFactoryData _ufoFactoryData;
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private AsteroidData _asteroidDataSmall;
        [SerializeField] private LaserData _laserData;
        
        public override void InstallBindings()
        {
            Container.Bind<LaserRenderer>().FromInstance(_laserRenderer).AsSingle();
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
            Container.Bind<SpawnTransform>().FromInstance(_spawnTransform).AsSingle();
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.Bind<AsteroidFactory>().AsSingle().WithArguments(_asteroidFactoryData, _asteroidData, _asteroidDataSmall);
            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletFactoryData);
            Container.Bind<UfoFactory>().AsSingle().WithArguments(_ufoFactoryData);
            Container.Bind<ScoreInfo>().AsSingle();
            Container.Bind<GameStateController>().AsSingle();
            Container.Bind<IMovement>().To<KeyboardMovement>().AsSingle();
            Container.Bind<IShoot>().To<KeyboardShoot>().AsSingle();
            Container.Bind<Laser>().AsSingle().WithArguments(_laserData);   
            
            Container.Bind<IInitializable>().To<AsteroidFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<ScoreInfo>().FromResolve(); 
            Container.Bind<IInitializable>().To<UfoFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<Laser>().FromResolve(); 
            Container.Bind<IInitializable>().FromInstance(_endPanel); 

        }
    }
}