using _Project.Scripts.Infrastructure;
using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Interfaces;
using GameScene.Level;
using GameScene.Repositories;
using GameScene.Level.ScriptableObjects;

namespace GameSystem
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LaserData _laserData;
        [SerializeField] private SpawnPositionData _spawnPositionData;
        [SerializeField] private GameStateController _gameStateController;
        
        public override void InstallBindings()
        {
            Container.Bind<SaveService>().To<PrefsSave>().AsSingle();
            Container.Bind<UnloadAssets>().AsSingle();
            Container.Bind(typeof(LoadPrefab<>)).AsSingle();
            Container.Bind<GameData>().AsSingle();
            Container.Bind<SpawnTransform>().AsSingle().WithArguments(_spawnPositionData);
            Container.Bind<GameStateController>().FromInstance(_gameStateController).AsSingle();
            Container.Bind<IAnalyticService>().To<FirebaseAnalytic>().AsSingle();
            Container.Bind<Laser>().AsSingle().WithArguments(_laserData);   
            
            Container.Bind<IInitializable>().To<SaveService>().FromResolve(); 
            Container.Bind<IInitializable>().To<IAnalyticService>().FromResolve(); 
            Container.Bind<IInitializable>().To<Laser>().FromResolve(); 
        }
    }
}