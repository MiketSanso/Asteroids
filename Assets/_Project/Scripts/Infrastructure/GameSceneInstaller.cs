using GameScene.Common;
using GameScene.Entities.Player;
using GameScene.Factories;
using GameScene.Repositories;
using UnityEngine;
using Zenject;
using GameScene.Game;

namespace GameSystem.Common.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUI _player;
        [SerializeField] private TransformParent _transformParent;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle(); 
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UfoFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<ScoreRepository>().AsSingle(); 
            Container.Bind<IInputService>().To<KeyboardInput>().AsSingle();
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
        }
    }
}