using GameScene.Common;
using GameScene.Entities.Player;
using GameScene.Factories;
using GameScene.Models;
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
            Container.BindInterfacesTo<EntryPoint>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle(); 
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UfoFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<EndGamePresenter>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
        }
    }
}