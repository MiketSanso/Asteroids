using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Common;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Common.DataSaveSystem;
using GameScene.Models;
using GameSystem.Common.LoadAssetSystem;
using DataPresenter = GameScene.Common.DataSaveSystem.DataPresenter;

namespace GameSystem.Common.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameEventBus gameEventBus;
        [SerializeField] private MusicService _musicService;
        
        public override void InstallBindings()
        {
            ISaveService localSaveService = new PrefsSave();
            ISaveService globalSaveService = new CloudSave();

            Container.BindInterfacesTo<UnityAdsInitializer>().AsSingle();
            Container.BindInterfacesTo<Authentication>().AsSingle(); 
            Container.Bind<ScoreModel>().AsSingle();
            Container.Bind<GameEventBus>().FromInstance(gameEventBus).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityBuyNoAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<DataPresenter>().AsSingle().WithArguments(localSaveService, globalSaveService);
            Container.BindInterfacesAndSelfTo<ConfigFirebaseLoad>().AsSingle();
            Container.Bind<MusicService>().FromInstance(_musicService).AsSingle();
            Container.Bind(typeof(AddressablePrefabLoader<>)).AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnTransform>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UnloadAssets>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<FirebaseAnalytic>().AsSingle();
            Container.BindInterfacesAndSelfTo<Laser>().AsSingle(); 
        }
    }
}