using System;
using GameScene.Entities.Player;
using GameScene.Factories;
using GameScene.Interfaces;
using GameScene.Repositories;
using UnityEngine;
using Zenject;
using GameScene.Level;

namespace GameSystem.Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUI _player;
        [SerializeField] private TransformParent _transformParent;
        
        public override void InstallBindings()
        {
            Container.Bind<AsteroidFactory>().AsSingle();
            Container.Bind<EntryPoint>().AsSingle();
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.Bind<BulletFactory>().AsSingle();
            Container.Bind<UfoFactory>().AsSingle();
            Container.Bind<ScoreRepository>().AsSingle();
            Container.Bind<IInputService>().To<KeyboardInput>().AsSingle();
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
            
            Container.Bind<IInitializable>().To<UfoFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<AsteroidFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<BulletFactory>().FromResolve(); 
            Container.Bind<IInitializable>().To<EntryPoint>().FromResolve(); 
            Container.Bind<IInitializable>().To<ScoreRepository>().FromResolve(); 
            
            Container.Bind<IDisposable>().To<UfoFactory>().FromResolve(); 
            Container.Bind<IDisposable>().To<AsteroidFactory>().FromResolve(); 
            Container.Bind<IDisposable>().To<ScoreRepository>().FromResolve(); 
        }
    }
}