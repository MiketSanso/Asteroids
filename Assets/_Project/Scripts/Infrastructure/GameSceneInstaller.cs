using GameScene.Entities.Player;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;
using GameScene.Repositories;
using GameScene.Factories;
using GameScene.Factories.ScriptableObjects;

namespace GameScene.Level
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUI _player;
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private Shoot _shoot;
        [SerializeField] private SpawnTransform _spawnTransform;
        [SerializeField] private TransformParent _transformParent;
        [SerializeField] private AsteroidFactoryData _asteroidFactoryData;
        [SerializeField] private BulletFactoryData _bulletFactoryData;
        [SerializeField] private UfoFactoryData _ufoFactoryData;
        
        public override void InstallBindings()
        {
            Container.Bind<EndPanel>().FromInstance(_endPanel).AsSingle();
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
            Container.Bind<Shoot>().FromInstance(_shoot).AsSingle();
            Container.Bind<SpawnTransform>().FromInstance(_spawnTransform).AsSingle();
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletFactoryData);
            Container.Bind<UfoFactory>().AsSingle().WithArguments(_ufoFactoryData);
            Container.Bind<AsteroidFactory>().AsSingle().WithArguments(_asteroidFactoryData);
            Container.Bind<ScoreInfo>().AsSingle();
            Container.Bind<IMovement>().To<KeyboardMovement>().AsSingle();
        }
    }
}