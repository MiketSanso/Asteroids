using _Project.Scripts.Infrastructure;
using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Interfaces;
using GameScene.Level;
using GameScene.Repositories;

namespace GameSystem
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private MusicService _musicService;
        
        public async override void InstallBindings()
        {
            ILocalSaveService localSaveService = new PrefsLocalSave();
            
            Container.Bind<SaveService>().To<CloudSave>().AsSingle().WithArguments(localSaveService);
            Container.Bind<ConfigSaveService>().To<ConfigFirebaseSave>().AsSingle();
            Container.Bind<MusicService>().FromInstance(_musicService).AsSingle();
            Container.Bind<UnloadAssets>().AsSingle();
            Container.Bind(typeof(LoadPrefab<>)).AsSingle();
            Container.Bind<GameData>().AsSingle();
            Container.Bind<SpawnTransform>().AsSingle();
            Container.Bind<GameStateController>().FromInstance(_gameStateController).AsSingle();
            Container.Bind<IAnalyticService>().To<FirebaseAnalytic>().AsSingle();
            Container.Bind<Laser>().AsSingle();   
            
            Container.Bind<IInitializable>().To<SpawnTransform>().FromResolve(); 
            Container.Bind<IInitializable>().To<SaveService>().FromResolve(); 
            Container.Bind<IInitializable>().To<IAnalyticService>().FromResolve(); 
            Container.Bind<IInitializable>().To<Laser>().FromResolve(); 
            
            Authentication authentication = new Authentication();
            await authentication.Auth();
        }
    }
}