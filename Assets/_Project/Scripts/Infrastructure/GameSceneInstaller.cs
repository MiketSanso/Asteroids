using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;
using GameScene.Repositories;
using GameScene.Factories;
using GameScene.Factories.ScriptableObjects;
using GameScene.Level.ScriptableObjects;

namespace GameScene.Level
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Canvas prefabGameCanvas;
        [SerializeField] private PlayerUI _player;
        [SerializeField] private TransformParent _transformParent;
        [SerializeField] private AsteroidFactoryData _asteroidFactoryData;
        [SerializeField] private BulletFactoryData _bulletFactoryData;
        [SerializeField] private UfoFactoryData _ufoFactoryData;
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private AsteroidData _asteroidDataSmall;
        [SerializeField] private LaserData _laserData;
        [SerializeField] private UfoData _ufoData;
        [SerializeField] private SpawnPositionData _spawnPositionData;
        
        public override void InstallBindings()
        {
            Container.Bind<EntryPoint>().AsSingle().WithArguments(prefabGameCanvas);
            Container.Bind<ScoreInfo>().AsSingle();
            Container.Bind<SpawnTransform>().AsSingle().WithArguments(_spawnPositionData);
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.Bind<AsteroidFactory>().AsSingle().WithArguments(_asteroidFactoryData, _asteroidData, _asteroidDataSmall);
            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletFactoryData);
            Container.Bind<UfoFactory>().AsSingle().WithArguments(_ufoFactoryData, _ufoData);
            Container.Bind<GameStateController>().AsSingle();
            Container.Bind<IInputSystem>().To<KeyboardInput>().AsSingle();
            Container.Bind<Laser>().AsSingle().WithArguments(_laserData);   
            
            Container.Bind<IInitializable>().To<EntryPoint>().FromResolve(); 
            Container.Bind<IInitializable>().To<ScoreInfo>().FromResolve(); 
            Container.Bind<IInitializable>().To<AsteroidFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<UfoFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<Laser>().FromResolve(); 
        }
    }
}