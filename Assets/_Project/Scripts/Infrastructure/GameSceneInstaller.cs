using GameScene.Entities.Player;
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

        public override void InstallBindings()
        {
            Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
            Container.Bind<EndPanel>().FromInstance(_endPanel).AsSingle();
            Container.Bind<Shoot>().FromInstance(_shoot).AsSingle();
            Container.Bind<SpawnTransform>().FromInstance(_spawnTransform).AsSingle();
        }
    }
}