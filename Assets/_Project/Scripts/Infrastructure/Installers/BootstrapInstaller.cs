using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Infrastructure;
using GameScene.Infrastructure.ConfigSaveSystem;
using GameScene.Infrastructure.DataSaveSystem;
using GameScene.Interfaces;
using GameScene.Repositories;
using GameSystem.Infrastructure.LoadAssetSystem;

namespace GameSystem.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private MusicService _musicService;
        
        public override void InstallBindings()
        {
            ILocalSaveService localSaveService = new PrefsLocalSave();
            
            Container.Bind<SaveService>().To<CloudSave>().AsSingle().WithArguments(localSaveService);
            Container.Bind<ConfigSaveService>().To<ConfigFirebaseSave>().AsSingle();
            Container.Bind<MusicService>().FromInstance(_musicService).AsSingle();
            Container.Bind(typeof(LoadPrefab<>)).AsSingle();
            Container.Bind<GameData>().AsSingle();
            Container.Bind<SpawnTransform>().AsSingle();
            Container.Bind<GameStateController>().FromInstance(_gameStateController).AsSingle();
            Container.Bind<UnloadAssets>().AsSingle();
            Container.Bind<IAnalyticService>().To<FirebaseAnalytic>().AsSingle();
            Container.Bind<Laser>().AsSingle();   
            
            Container.Bind<IInitializable>().To<UnloadAssets>().FromResolve(); 
            Container.Bind<IInitializable>().To<SpawnTransform>().FromResolve(); 
            Container.Bind<IInitializable>().To<SaveService>().FromResolve(); 
            Container.Bind<IInitializable>().To<IAnalyticService>().FromResolve(); 
            Container.Bind<IInitializable>().To<Laser>().FromResolve(); 
            
            Authentication authentication = new Authentication();
            authentication.Auth().Forget();
        }
    }
}