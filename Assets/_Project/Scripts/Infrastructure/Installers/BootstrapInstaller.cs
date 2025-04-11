using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Level;
using GameScene.Repositories;
using GameScene.Level.ScriptableObjects;

namespace GameScene.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LaserData _laserData;
        [SerializeField] private SpawnPositionData _spawnPositionData;
        
        public override void InstallBindings()
        {
            Container.Bind<GameData>().AsSingle();
            Container.Bind<SpawnTransform>().AsSingle().WithArguments(_spawnPositionData);
            Container.Bind<GameStateController>().AsSingle();
            Container.Bind<Laser>().AsSingle().WithArguments(_laserData);   
            
            Container.Bind<IInitializable>().To<GameData>().FromResolve(); 
            Container.Bind<IInitializable>().To<Laser>().FromResolve(); 
        }
    }
}