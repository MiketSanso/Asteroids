using GameScene.Entities.Player;
using GameScene.Interfaces;
using UnityEngine;
using Zenject;

namespace GameScene.Level
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerUI _player;
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private Shoot _shoot;
        [SerializeField] private SpawnTransform _spawnTransform;
        [SerializeField] private TransformParent _transformParent;
        [SerializeField] private PlayerMovement _playerMovement;

        private readonly KeyboardMovement _keyboardMovement = new KeyboardMovement();

        public override void InstallBindings()
        {
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
            Container.Bind<EndPanel>().FromInstance(_endPanel).AsSingle();
            Container.Bind<Shoot>().FromInstance(_shoot).AsSingle();
            Container.Bind<SpawnTransform>().FromInstance(_spawnTransform).AsSingle();
            Container.Bind<TransformParent>().FromInstance(_transformParent).AsSingle();
            Container.Bind<IMovement>().FromInstance(_keyboardMovement).AsSingle();
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle();
            
            Container.Inject(_keyboardMovement);
        }
    }
}