using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Common.DataSaveSystem;
using GameScene.Repositories;
using GameSystem.Common.LoadAssetSystem;

namespace GameSystem.Common.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameStateController _gameStateController;
        [SerializeField] private MusicService _musicService;
        
        public override void InstallBindings()
        {
            ILocalSaveService localSaveService = new PrefsSave();
            IGlobalSaveService globalSaveService = new CloudSave();

            Container.BindInterfacesAndSelfTo<UnityAdsInitializer>().AsSingle();
            Container.Bind<IInterstitialAdsService>().To<UnityInterstitialAds>().AsSingle();
            Container.Bind<IRewardedAdsService>().To<UnityRewardedAds>().AsSingle();
            Container.Bind<IBuyNoAdsService>().To<UnityBuyNoAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle().WithArguments(localSaveService, globalSaveService);
            Container.Bind<ConfigLoadService>().To<ConfigFirebaseLoad>().AsSingle();
            Container.Bind<MusicService>().FromInstance(_musicService).AsSingle();
            Container.Bind(typeof(AddressablePrefabLoader<>)).AsSingle();
            Container.Bind<GameData>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnTransform>().AsSingle(); 
            Container.Bind<GameStateController>().FromInstance(_gameStateController).AsSingle();
            Container.BindInterfacesAndSelfTo<UnloadAssets>().AsSingle(); 
            Container.Bind<IAnalyticService>().To<FirebaseAnalytic>().AsSingle();
            Container.BindInterfacesAndSelfTo<Laser>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<Authentication>().AsSingle(); 
            
            Container.BindInterfacesTo<IAnalyticService>().FromResolve(); 
            Container.BindInterfacesTo<IInterstitialAdsService>().FromResolve(); 
            Container.BindInterfacesTo<IRewardedAdsService>().FromResolve(); 
            Container.BindInterfacesTo<IBuyNoAdsService>().FromResolve(); 
        }
    }
}