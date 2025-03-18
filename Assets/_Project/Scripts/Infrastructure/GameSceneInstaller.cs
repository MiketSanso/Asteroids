using GameScene.Entities.Asteroid;
using GameScene.Entities.Player;
using GameScene.Entities.UFOs;
using GameScene.Level;
using UnityEngine;
using GameScene.Repositories;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerUI _player;
    [SerializeField] private EndPanel _endPanel;
    [SerializeField] private Shoot _shoot;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerUI>().FromInstance(_player).AsSingle();
        Container.Bind<EndPanel>().FromInstance(_endPanel).AsSingle();
        Container.Bind<Shoot>().FromInstance(_shoot).AsSingle();
    }
}