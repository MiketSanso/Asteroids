using UnityEngine;
using Zenject;
using GameScene.Entities.Player;
using GameScene.Common;
using GameScene.Common.ChangeSceneService;
using GameScene.Common.ConfigSaveSystem;
using GameScene.Common.DataSaveSystem;
using GameScene.Models;
using GameSystem.Common.LoadAssetSystem;

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

            Container.Bind<SceneChanger>().AsSingle();
            Container.BindInterfacesTo<Bootstrap>().AsSingle();
            Container.BindInterfacesTo<UnityAdsInitializer>().AsSingle();
            Container.BindInterfacesTo<Authentication>().AsSingle(); 
            Container.Bind<ScoreModel>().AsSingle();
            Container.Bind<GameEventBus>().FromInstance(gameEventBus).AsSingle();
            Container.BindInterfacesTo<UnityAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityBuyNoAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<DataService>().AsSingle().WithArguments(localSaveService, globalSaveService);
            Container.BindInterfacesAndSelfTo<DataPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConfigFirebaseLoad>().AsSingle();
            Container.Bind(typeof(AddressablePrefabLoader<>)).AsSingle(); 
            Container.Bind<MusicService>().FromInstance(_musicService).AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnTransform>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UnloadAssets>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<FirebaseAnalytic>().AsSingle();
            Container.BindInterfacesAndSelfTo<Laser>().AsSingle();
        }
    }
}