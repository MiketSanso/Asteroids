using GameScene.Common;
using GameScene.Entities.PlayerSpace;
using GameScene.Factories;
using GameScene.Models;
using UnityEngine;
using Zenject;
using GameScene.Game;

namespace GameSystem.Common.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private TransformParent _transformParent;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle(); 
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UfoFactory>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.BindInterfacesAndSelfTo<EndGamePresenter>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
            Container.BindInterfacesTo<EntryPoint>().AsSingle(); 
        }
    }
}